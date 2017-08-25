using Microsoft.Practices.Unity;
using OmniBot.APIModels;
using OmniBot.APIModels.Enums;
using OmniBot.DataClasses;
using OmniBot.DataModel;
using OmniBot.Services.Exceptions;
using OmniBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OmniBot.Services
{
    public class ConversationService : IConversationService
    {
        [Dependency]
        public OmniBotContext Context { private get; set; }

        [Dependency]
        public IScriptService ScriptService { private get; set; }

        [Dependency]
        public IIntentService IntentTypeService { private get; set; }

        public Conversation CreateConversation(ConversationStarter conversationStarter, Person person)
        {
            Conversation conversation = GetActiveConversationForPerson(person);

            if (conversation != null)
            {
                throw new OmniBotException(person + " already has an active conversation and cannot be started on a new one.");
            }

            DataClasses.Script script = ScriptService.GetScriptByConversationStarter(conversationStarter);

            conversation = new Conversation()
            {
                Id = -1,
                Person = person,
                Script = script,
                StatusId = StatusService.getInstance().conversationStatuses[StatusType.STARTED.ToString()].Id,
                Active = true
            };

            Context.Conversations.Add(conversation);

            return conversation;
        }


        public Conversation GetActiveConversationForPerson(Person person)
        {
            return GetActiveConversationForPersonId(person.Id);
        }

        public Conversation GetActiveConversationForPersonId(int id)
        {
            Conversation conversation = Context.Conversations.Where(n => n.PersonId == id
                    && n.Active == true).SingleOrDefault<Conversation>();

            if (conversation != null)
            {
                Context.Entry(conversation).Collection(c => c.Communications).Load();
                Context.Entry(conversation).Reference(c => c.Script).Load();
                Context.Entry(conversation).Reference(c => c.CurrentMessage).Load();
            }

            return conversation;
        }

        public void UpdateCurrentMessage(Conversation conversation, DataClasses.Message nextMessage, AiResponseExtract response)
        {
            if (response.Intent.Equals(IntentTypeService.GetMisunderstoodIntent().Name)
                || response.SmallTalk)
            {
                // TODO increment times user got stuck.  If at a certain amount, Trigger move on.
                // Move on will send an extra message.  Need to determine how to store the extra message in the DB.
                return;
            }

            conversation.CurrentMessage = nextMessage;

            if (nextMessage.IsEnd)
            {
                conversation.Active = false;
            }
        }

    }
}
