using OmniBot.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services.Interfaces
{
    public interface IOmniBotService
    {
        void StartConversation(ConversationStarter conversationStarter);
        string MaintainConversation(UserRequest incomingText);
    }
}
