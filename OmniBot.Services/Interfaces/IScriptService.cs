using OmniBot.APIModels;
using OmniBot.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services.Interfaces
{
    public interface IScriptService
    {
        DataClasses.Message GetFirstMessageFromScript(DataClasses.Script script);
        DataClasses.Script GetScriptByConversationStarter(ConversationStarter conversationStarter);
        DataClasses.Script GetScriptById(int id);
        DataClasses.Script GetScriptByName(string name);
        DataClasses.Script FinalizeAndSaveScript(DataClasses.Script uploadedScript);
    }
}
