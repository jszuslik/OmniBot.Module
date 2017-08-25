using OmniBot.APIModels;
using OmniBot.DataClasses;
using OmniBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services
{
    public class CommunicationService : ICommunicationService
    {
        public Communication CreateIncomingCommunication(Conversation conversation, UserRequest userRequest, AiResponseExtract aiResponse)
        {

            Communication comm = new Communication()
            {
                Conversation = conversation,
                Outbound = false,
                Body = userRequest.Body,
                Context = aiResponse.Intent
            };

            return comm;
        }

        public Communication CreateOutgoingCommunication(Conversation conversation, DataClasses.Message message)
        {
            Communication comm = new Communication()
            {
                Conversation = conversation,
                Outbound = true,
                Body = message.Body,
                Context = message.Name
            };

            return comm;
        }
    }
}
