using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.APIModels
{
    public class ConversationStarter
    {
        public int ScriptId { get; set; }

        public string ScriptName { get; set; }

        public int PersonId { get; set; }

        public string PhoneNumber { get; set; }
    }
}
