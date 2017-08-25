using Microsoft.Practices.Unity;
using OmniBot.APIModels;
using OmniBot.DataClasses;
using OmniBot.DataModel;
using OmniBot.Services.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace OmniBot.Services
{
    public class ScriptService : IScriptService
    {
        [Dependency]
        public OmniBotContext Context { private get; set; }

        [Dependency]
        public MessageService MessageService { private get; set; }

        public DataClasses.Script GetScriptByConversationStarter(ConversationStarter conversationStarter)
        {
            DataClasses.Script script = null;

            if (conversationStarter.ScriptName != null)
            {
                script = GetScriptByName(conversationStarter.ScriptName);
            }

            if (script == null)
            {
                script = GetScriptById(conversationStarter.ScriptId);
            }

            return script;
        }

        public DataClasses.Script GetScriptById(int id)
        {
            return Context.Scripts.Where(n => n.Id == id).Include("Messages").SingleOrDefault<DataClasses.Script>();
        }

        public DataClasses.Script GetScriptByName(string name)
        {
            return Context.Scripts.Where(n => n.Name == name).Include("Messages").SingleOrDefault<DataClasses.Script>();
        }

        public DataClasses.Message GetFirstMessageFromScript(DataClasses.Script script)
        {
            if (script == null)
            {
                throw new NullReferenceException("Script is null");
            }

            foreach (DataClasses.Message message in script.Messages)
            {
                if (message.IsStart)
                {
                    return message;
                }
            }
            return null;
        }

        public DataClasses.Script FinalizeAndSaveScript(DataClasses.Script uploadedScript)
        {
            DataClasses.Script currentScript = GetScriptById(uploadedScript.Id);

            if (currentScript != null)
            {
                currentScript.Name = uploadedScript.Name;
                MessageService.MergeMessages(currentScript.Messages, uploadedScript.Messages);
            }
            else
            {
                currentScript = uploadedScript;
                Context.Scripts.Add(currentScript);
            }

            MessageService.FinalizeTransitioners(currentScript.Messages);

            Context.SaveChanges();
            return currentScript;
        }
    }
}
