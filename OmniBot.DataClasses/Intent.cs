using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniBot.DataClasses
{
    public class Intent : ModelBase
    {
        public Intent() : base()
        {

        }
        public Intent(Intent Intent) : base(Intent)
        {

        }

        [Required(ErrorMessage = "An Intent Type must have a name")]
        [Column("name")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
