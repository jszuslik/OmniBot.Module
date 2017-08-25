using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniBot.DataClasses
{
    public class Message : ModelBase
    {
        public Message() : base()
        {

        }
        public Message(Message Message) : base(Message)
        {

        }

        [ForeignKey("ScriptId")]
        public Script Script { get; set; }

        [Column("script_id")]
        [Index("script_name", 1, IsUnique = true)]
        public int ScriptId { get; set; }

        [Required(ErrorMessage = "A Message must have a name")]
        [Column("name")]
        [StringLength(100)]
        [Index("script_name", 2, IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "A Message must have a body")]
        [StringLength(2000)]
        [Column("body")]
        public String Body { get; set; }

        [DefaultValue("'FALSE'")]
        [Column("starts_conversation")]
        public bool IsStart { get; set; }

        [DefaultValue("'FALSE'")]
        [Column("ends_conversation")]
        public bool IsEnd { get; set; }

        public List<Transition> Transitions { get; set; }
        public List<Transition> AddTransitions(Transition transition)
        {
            if(this.Transitions == null)
            {
                this.Transitions = new List<Transition>();
            }
            this.Transitions.Add(transition);
            return this.Transitions;
        }
    }
}
