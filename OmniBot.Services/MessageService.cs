using Microsoft.Practices.Unity;
using OmniBot.APIModels;
using OmniBot.DataClasses;
using OmniBot.DataModel;
using OmniBot.Services.Exceptions;
using OmniBot.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OmniBot.Services
{
    public class MessageService : IMessageService
    {
        [Dependency]
        public OmniBotContext Context { private get; set; }

        [Dependency]
        public IIntentService IntentService { private get; set; }

        private const string INVALID_MISUNDERSTOOD_MESSAGE = "A Misunderstood Message can only have a single \"move-on\" Intent Type.";
        private const string MULTIPLE_FALLBACKS = "A Message can only have a single \"Fallback\" Intent Type (misunderstood or accept-any)";

        private const string DEFAULT_MISUNDERSTOOD_BODY = "I'm sorry, I didn't get that.";

        public DataClasses.Message GetNextMessage(Conversation conversation, AiResponseExtract response)
        {
            DataClasses.Message nextMessage = null;

            var query = from MessageTransitioner in Context.Transitions
                        where MessageTransitioner.CurrentMessage.Name == conversation.CurrentMessage.Name
                        && MessageTransitioner.Intent.Name == response.Intent
                        select MessageTransitioner.NextMessage;

            nextMessage = query.SingleOrDefault();

            if (nextMessage == null)
            {
                nextMessage = CreateSmallTalkMessage(conversation, response);
            }

            return nextMessage;
        }

        private DataClasses.Message CreateSmallTalkMessage(Conversation conversation, AiResponseExtract response)
        {
            DataClasses.Message smallTalkMessage = new DataClasses.Message()
            {
                Name = conversation.CurrentMessage.Name + "-smalltalk",
                Body = response.Speech,
                IsStart = false,
                IsEnd = false
            };

            return smallTalkMessage;
        }

        internal void MergeMessages(List<DataClasses.Message> currentMessages, List<DataClasses.Message> uploadedMessages)
        {
            List<DataClasses.Message> deleteMessages = new List<DataClasses.Message>();

            // Update existing messages
            foreach (DataClasses.Message currentMessage in currentMessages)
            {
                DataClasses.Message uploadedMessage = uploadedMessages.FirstOrDefault(m => m.Id == currentMessage.Id);

                if (uploadedMessage == null)
                { // Delete message
                    deleteMessages.Add(currentMessage);
                }
                else
                { // Update message
                    currentMessage.Name = uploadedMessage.Name;
                    currentMessage.Body = uploadedMessage.Body;
                    currentMessage.IsStart = uploadedMessage.IsStart;
                    currentMessage.IsEnd = uploadedMessage.IsEnd;
                    currentMessage.Transitions = uploadedMessage.Transitions;

                    uploadedMessages.Remove(uploadedMessage);
                }
            }

            Context.Messages.RemoveRange(deleteMessages);

            List<DataClasses.Message> newMessages = new List<DataClasses.Message>();
            // Add new messages
            foreach (DataClasses.Message uploadedMessage in uploadedMessages)
            {
                if (currentMessages.FirstOrDefault(m => m.Name.Equals(uploadedMessage.Name)) == null)
                {
                    newMessages.Add(uploadedMessage);
                }
            }

            currentMessages.AddRange(newMessages);
        }

        internal void FinalizeTransitioners(List<DataClasses.Message> messages)
        {
            List<DataClasses.Message> newMisunderstoodMessages = new List<DataClasses.Message>();

            foreach (DataClasses.Message message in messages)
            {
                bool createMisunderstood = HandleTransitionerList(message);
                if (createMisunderstood)
                {
                    DataClasses.Message misunderstoodMessage = CreateMisunderstoodMessage(message);
                    newMisunderstoodMessages.Add(misunderstoodMessage);

                    message.Transitions.Add(new DataClasses.Transition()
                    {
                        CurrentMessage = message,
                        Intent = IntentService.GetMisunderstoodIntent(),
                        NextMessage = misunderstoodMessage
                    });
                }
            }

            messages.AddRange(newMisunderstoodMessages);

            UpdateNullNextMessages(messages);
        }

        private DataClasses.Message CreateMisunderstoodMessage(DataClasses.Message parentMessage)
        {
            DataClasses.Message misunderstoodMessage = new DataClasses.Message()
            {
                Name = parentMessage.Name + "-misunderstood",
                Body = DEFAULT_MISUNDERSTOOD_BODY + " " + parentMessage.Name, // TODO remove test code on body
                IsStart = false,
                IsEnd = false
            };

            DataClasses.Transition transition = new DataClasses.Transition()
            {
                CurrentMessage = misunderstoodMessage,
                Intent = IntentService.GetMoveOnIntent(),
                NextMessage = parentMessage // Default, move on to the current message
            };

            misunderstoodMessage.Transitions = new List<DataClasses.Transition>
            {
                transition
            };
            return misunderstoodMessage;
        }

        /// <summary>
        /// Method that validates whether or not a Messages Transitioners are set up correctly.  Returns 
        /// a boolean that represents whether or not this Message needs to have a new Message created for 
        /// the misunderstoodIntentType. 
        /// </summary>
        /// <param name="message"></param>
        /// <returns>a boolean that represents whether or not this Message needs to have a new Message 
        /// created for the misunderstood IntentType. Always returns false if this is the misunderstood 
        /// Message.</returns>
        private bool HandleTransitionerList(DataClasses.Message message)
        {
            List<DataClasses.Transition> transitioners = message.Transitions;

            bool isFallback = transitioners.Find(t => t.IntentId ==
                IntentService.GetMoveOnIntent().Id) == null ? false : true;

            if (isFallback)
            {
                if (transitioners.Count > 1)
                {
                    throw new OmniBotException(INVALID_MISUNDERSTOOD_MESSAGE);
                }
                return false;
            }

            bool hasAcceptAny = transitioners.Find(t => t.IntentId ==
                IntentService.GetAcceptAnyIntent().Id) == null ? false : true;

            bool hasMisunderstood = transitioners.Find(t => t.IntentId ==
                IntentService.GetMisunderstoodIntent().Id) == null ? false : true;

            if (hasAcceptAny && hasMisunderstood)
            {
                throw new OmniBotException(MULTIPLE_FALLBACKS);
            }

            if (!hasAcceptAny && !hasMisunderstood && !message.IsEnd)
            {
                System.Diagnostics.Trace.WriteLine("Setting to create a new misunderstood");
                return true;
            }

            return false;
        }

        private void UpdateNullNextMessages(List<DataClasses.Message> messages)
        {
            foreach (DataClasses.Message message in messages)
            {
                foreach (DataClasses.Transition transition in message.Transitions)
                {
                    if (transition.NextMessage == null)
                    {
                        transition.NextMessage = messages.Find(m => m.Id == transition.NextMessageId);
                    }
                }
            }
        }
    }
}
