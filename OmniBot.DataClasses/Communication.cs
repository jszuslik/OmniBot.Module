using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniBot.DataClasses
{
    public class Communication : ModelBase
    {
        public Communication() : base()
        {

        }
        public Communication(Communication Communication) : base(Communication)
        {

        }

        [Required]
        [ForeignKey("ConversationId")]
        public Conversation Conversation { get; set; }

        [Required]
        [Column("conversation_id")]
        public int ConversationId { get; set; }

        [DefaultValue("'FALSE'")]
        [Column("outbound")]
        public bool Outbound { get; set; }

        [Required]
        [Column("body")]
        [StringLength(1000)]
        public string Body { get; set; }

        [Required]
        [Column("context")]
        [StringLength(100)]
        public string Context { get; set; }
    }
}
