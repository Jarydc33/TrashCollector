namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAccountSuspension : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "AccountSuspended", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "AccountSuspended");
        }
    }
}
