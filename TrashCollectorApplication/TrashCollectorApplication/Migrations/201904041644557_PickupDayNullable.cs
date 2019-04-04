namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PickupDayNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "PickupDayId", "dbo.PickupDays");
            DropIndex("dbo.Clients", new[] { "PickupDayId" });
            AlterColumn("dbo.Clients", "PickupDayId", c => c.Int());
            CreateIndex("dbo.Clients", "PickupDayId");
            AddForeignKey("dbo.Clients", "PickupDayId", "dbo.PickupDays", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "PickupDayId", "dbo.PickupDays");
            DropIndex("dbo.Clients", new[] { "PickupDayId" });
            AlterColumn("dbo.Clients", "PickupDayId", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "PickupDayId");
            AddForeignKey("dbo.Clients", "PickupDayId", "dbo.PickupDays", "id", cascadeDelete: true);
        }
    }
}
