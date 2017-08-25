using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.DataClasses
{
    public class Conversation : ModelBase
    {
        public Conversation() : base()
        {

        }
        public Conversation(Conversation Conversation) : base(Conversation)
        {

        }

        [Required]
        [ForeignKey("ScriptId")]
        public Script Script { get; set; }

        [Column("script_id")]
        public int ScriptId { get; set; }

        [Required]
        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [Required]
        [Column("person_id")]
        public int PersonId { get; set; }

        [Required]
        [ForeignKey("CurrentMessageId")]
        public Message CurrentMessage { get; set; }

        [Required]
        [Column("current_message_id")]
        public int CurrentMessageId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        [Required]
        [Column("status_id")]
        public int StatusId { get; set; }

        [Required]
        [Column("active")]
        public bool Active { get; set; }


        public List<Communication> Communications { get; set; }

    }
}
