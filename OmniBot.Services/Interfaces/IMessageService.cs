using OmniBot.APIModels;
using OmniBot.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services.Interfaces
{
    public interface IMessageService
    {
        DataClasses.Message GetNextMessage(Conversation conversation, AiResponseExtract response);
    }
}
