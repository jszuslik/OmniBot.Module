using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.APIModels
{
    public class AiResponseExtract
    {

        public AiResponseExtract()
        {
            Contexts = new List<string>();
        }

        public string Intent { get; set; }

        public string Speech { get; set; }

        public bool SmallTalk { get; set; } = false;

        public IEnumerable<string> Contexts { get; set; }

    }
}
