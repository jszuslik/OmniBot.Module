using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.DataClasses
{
    public abstract class ModelBase
    {
        protected ModelBase()
        {
            this.CreatedOn = DateTime.Now;
        }
        protected ModelBase(ModelBase modelBase)
        {
            Id = modelBase.Id;
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DefaultValue("getDate()")]
        [Column("created_on")]
        public DateTime CreatedOn { get; set; }
    }
}
