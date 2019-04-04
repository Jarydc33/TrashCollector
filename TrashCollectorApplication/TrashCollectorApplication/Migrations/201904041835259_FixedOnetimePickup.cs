namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedOnetimePickup : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Clients", name: "OneTimePickupDay_id", newName: "OneTimePickupDayId");
            RenameIndex(table: "dbo.Clients", name: "IX_OneTimePickupDay_id", newName: "IX_OneTimePickupDayId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Clients", name: "IX_OneTimePickupDayId", newName: "IX_OneTimePickupDay_id");
            RenameColumn(table: "dbo.Clients", name: "OneTimePickupDayId", newName: "OneTimePickupDay_id");
        }
    }
}
