using OmniBot.DataClasses;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace OmniBot.DataModel
{
    public class OmniBotContext : DbContext
    {
        public OmniBotContext() : base("OmniBot")
        {

        }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Intent> Intents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Transition> Transitions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasMany(t => t.Transitions).WithRequired(a => a.CurrentMessage).WillCascadeOnDelete(false);
            modelBuilder.Entity<Conversation>().HasRequired(t => t.CurrentMessage).WithMany().WillCascadeOnDelete(false);

            var convention = new AttributeToColumnAnnotationConvention<DefaultValueAttribute, string>("SqlDefaultValue",
                (p, attributes) => attributes.SingleOrDefault().Value.ToString());
            modelBuilder.Conventions.Add(convention);
        }
    }
}
