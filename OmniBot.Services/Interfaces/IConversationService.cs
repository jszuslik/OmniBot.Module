using OmniBot.APIModels;
using OmniBot.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services.Interfaces
{
    public interface IConversationService
    {
        Conversation CreateConversation(ConversationStarter conversationStarter, Person person);
        Conversation GetActiveConversationForPerson(Person person);
        Conversation GetActiveConversationForPersonId(int id);
        void UpdateCurrentMessage(Conversation conversation, DataClasses.Message nextMessage, AiResponseExtract response);
    }
}
