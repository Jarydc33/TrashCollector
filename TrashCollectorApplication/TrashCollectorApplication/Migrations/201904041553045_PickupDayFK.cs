namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PickupDayFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "PickupDayid", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "PickupDayid");
            AddForeignKey("dbo.Clients", "PickupDayid", "dbo.PickupDays", "id", cascadeDelete: true);
            DropColumn("dbo.Clients", "PickupDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "PickupDay", c => c.String());
            DropForeignKey("dbo.Clients", "PickupDayid", "dbo.PickupDays");
            DropIndex("dbo.Clients", new[] { "PickupDayid" });
            DropColumn("dbo.Clients", "PickupDayid");
        }
    }
}
