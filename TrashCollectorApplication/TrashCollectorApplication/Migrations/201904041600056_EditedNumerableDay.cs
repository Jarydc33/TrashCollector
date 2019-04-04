namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedNumerableDay : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Clients", new[] { "PickupDayid" });
            CreateIndex("dbo.Clients", "PickupDayId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clients", new[] { "PickupDayId" });
            CreateIndex("dbo.Clients", "PickupDayid");
        }
    }
}
