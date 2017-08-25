using OmniBot.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.APIModels
{
    public class Transition
    {
        public int Id { get; set; }
        public int IntentId { get; set; }
        public int CurrentMessageId { get; set; }
        public int NextMessageId { get; set; }

        public Transition(DataClasses.Transition transition)
        {
            Id = transition.Id;
            IntentId = transition.IntentId;
            CurrentMessageId = transition.CurrentMessageId;
            NextMessageId = transition.NextMessageId;
        }
    }
}
