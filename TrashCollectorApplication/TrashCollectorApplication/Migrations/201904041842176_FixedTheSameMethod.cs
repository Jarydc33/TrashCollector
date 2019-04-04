namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedTheSameMethod : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "OneTimePickupDayId", "dbo.PickupDays");
            DropIndex("dbo.Clients", new[] { "OneTimePickupDayId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Clients", "OneTimePickupDayId");
            AddForeignKey("dbo.Clients", "OneTimePickupDayId", "dbo.PickupDays", "id");
        }
    }
}
