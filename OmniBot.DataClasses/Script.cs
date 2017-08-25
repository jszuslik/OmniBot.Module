using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.DataClasses
{
    public class Script : ModelBase
    {
        public Script() : base()
        {

        }
        public Script(Script Script) : base(Script)
        {

        }

        [Required(ErrorMessage = "A Script must have a name")]
        [Column("name")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public String Name { get; set; }

        public List<Message> Messages { get; set; }
        public List<Message> AddMessage(Message message)
        {
            if(this.Messages == null)
            {
                this.Messages = new List<Message>();
            }
            this.Messages.Add(message);
            return this.Messages;
        }
    }
}
