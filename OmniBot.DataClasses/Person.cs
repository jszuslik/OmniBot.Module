using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.DataClasses
{
    public class Person : ModelBase
    {
        public Person() : base()
        {

        }
        public Person(Person Person) : base(Person)
        {
            
        }
        [StringLength(255)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Column("last_name")]
        public string LastName { get; set; }

        [StringLength(20)]
        [Index(IsUnique = true)]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Column("email")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Column("is_active")]
        public Boolean IsActive { get; set; }
    }
}
