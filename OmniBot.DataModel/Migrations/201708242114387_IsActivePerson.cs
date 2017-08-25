namespace OmniBot.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActivePerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "is_active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "is_active");
        }
    }
}
