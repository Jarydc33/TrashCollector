namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedOneTimePickup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "OneTimePickupDay_id", c => c.Int());
            CreateIndex("dbo.Clients", "OneTimePickupDay_id");
            AddForeignKey("dbo.Clients", "OneTimePickupDay_id", "dbo.PickupDays", "id");
            DropColumn("dbo.Clients", "OneTimePickupDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "OneTimePickupDay", c => c.String());
            DropForeignKey("dbo.Clients", "OneTimePickupDay_id", "dbo.PickupDays");
            DropIndex("dbo.Clients", new[] { "OneTimePickupDay_id" });
            DropColumn("dbo.Clients", "OneTimePickupDay_id");
        }
    }
}
