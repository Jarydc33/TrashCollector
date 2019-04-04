namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedClientDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "PickupDate", c => c.String());
            AddColumn("dbo.Clients", "OneTimePickup", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clients", "OneTimePickupDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "OneTimePickupDate");
            DropColumn("dbo.Clients", "OneTimePickup");
            DropColumn("dbo.Clients", "PickupDate");
        }
    }
}
