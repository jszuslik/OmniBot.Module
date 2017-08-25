using Microsoft.Practices.Unity;
using OmniBot.APIModels;
using OmniBot.DataClasses;
using OmniBot.DataModel;
using OmniBot.Services.Interfaces;

namespace OmniBot.Services
{
    public class OmniBotService : IOmniBotService
    {
        [Dependency]
        public IMessagingService MessagingService { private get; set; }

        [Dependency]
        public OmniBotContext Context { private get; set; }

        [Dependency]
        public IPeopleService PersonService { private get; set; }

        [Dependency]
        public IConversationService ConversationService { private get; set; }

        [Dependency]
        public IScriptService ScriptService { private get; set; }

        [Dependency]
        public IMessageService MessageService { private get; set; }

        [Dependency]
        public ICommunicationService CommunicationService { private get; set; }

        [Dependency]
        public IAiService AiService { private get; set; }


        public void StartConversation(ConversationStarter conversationStarter) // TODO put this here?
        {
            Person person = PersonService.GetPersonByPhoneNumber(conversationStarter.PhoneNumber);
            Conversation conversation = ConversationService.CreateConversation(conversationStarter, person);
            DataClasses.Message firstMessage = ScriptService.GetFirstMessageFromScript(conversation.Script);

            conversation.CurrentMessage = firstMessage;

            Communication text = CommunicationService.CreateOutgoingCommunication(conversation, firstMessage);

            conversation.Communications.Add(text);
            Context.SaveChanges();

            MessagingService.SendMessage(person.PhoneNumber, firstMessage.Body);
        }

        public string MaintainConversation(UserRequest userRequest)
        {
            Person person = PersonService.GetPersonByUserRequest(userRequest);
            Conversation conversation = ConversationService.GetActiveConversationForPerson(person);

            if (conversation == null)
            { // TODO clean up / look for reactive chatbot
                return "You do not have an active conversation";
            }

            AiResponseExtract response = AiService.ProcessUserRequest(conversation, userRequest);

            // Handle incoming Text
            Communication comm = CommunicationService.CreateIncomingCommunication(conversation, userRequest, response);
            conversation.Communications.Add(comm);

            // Change State (Message)
            DataClasses.Message nextMessage = MessageService.GetNextMessage(conversation, response);
            ConversationService.UpdateCurrentMessage(conversation, nextMessage, response);

            // Handle outbound Text
            Communication newComm = CommunicationService.CreateOutgoingCommunication(conversation, nextMessage);
            conversation.Communications.Add(newComm);

            Context.SaveChanges();
            return nextMessage.Body;
        }
    }
}
