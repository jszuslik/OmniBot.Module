using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.DataClasses
{
    public class Transition : ModelBase
    {
        public Transition() : base()
        {

        }
        public Transition(Transition Transition) : base(Transition)
        {

        }

        [ForeignKey("IntentId")]
        public Intent Intent { get; set; }

        [Column("intent_id")]
        public int IntentId { get; set; }

        [Required(ErrorMessage = "A Message Transitioner must belong to a Message")]
        [ForeignKey("CurrentMessageId")]
        public Message CurrentMessage { get; set; }

        [Column("current_message_id")]
        public int CurrentMessageId { get; set; }

        [Required]
        [ForeignKey("NextMessageId")]
        public Message NextMessage { get; set; }

        [Column("next_message_id")]
        public int NextMessageId { get; set; }
    }
}
