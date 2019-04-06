namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedClientCity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "City", c => c.String());
            DropColumn("dbo.Clients", "OneTimePickup");
            DropColumn("dbo.Clients", "PickupComplete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "PickupComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clients", "OneTimePickup", c => c.Boolean(nullable: false));
            DropColumn("dbo.Clients", "City");
        }
    }
}
