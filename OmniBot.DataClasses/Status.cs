using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.DataClasses
{
    public class Status : ModelBase
    {
        public Status() : base()
        {

        }
        public Status(Status Status) : base(Status)
        {

        }

        [Required(ErrorMessage = "A Status must have a name")]
        [Column("name")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public String Name { get; set; }
    }
}
