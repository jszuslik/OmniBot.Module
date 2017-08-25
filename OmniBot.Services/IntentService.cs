using Microsoft.Practices.Unity;
using OmniBot.DataClasses;
using OmniBot.DataModel;
using OmniBot.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OmniBot.Services
{
    public class IntentService : IIntentService
    {
        public const string ACCEPT_ANY = "accept-any";
        public const string MISUNDERSTOOD = "misunderstood";
        public const string MOVE_ON = "move-on";

        [Dependency]
        public OmniBotContext Context { private get; set; }


        public Intent GetIntentById(int id)
        {
            return Context.Intents.Find(id);
        }

        public Intent GetMisunderstoodIntent()
        {
            return Context.Intents.Where(i => i.Name == MISUNDERSTOOD).FirstOrDefault();
        }

        public Intent GetMoveOnIntent()
        {
            return Context.Intents.Where(i => i.Name == MOVE_ON).FirstOrDefault();
        }

        public Intent GetAcceptAnyIntent()
        {
            return Context.Intents.Where(i => i.Name == ACCEPT_ANY).FirstOrDefault();
        }

        public List<Intent> GetAllIntents()
        {
            return Context.Intents.ToList();
        }
    }
}
