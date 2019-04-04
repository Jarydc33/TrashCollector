namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedPickupDay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PickupDays",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        GarbagePickupDay = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Clients", "PickupDay", c => c.String());
            AddColumn("dbo.Clients", "OneTimePickupDay", c => c.String());
            AddColumn("dbo.Clients", "ZipCode", c => c.String());
            AddColumn("dbo.Clients", "State", c => c.String());
            AddColumn("dbo.Clients", "Address", c => c.String());
            DropColumn("dbo.Clients", "PickupDate");
            DropColumn("dbo.Clients", "OneTimePickupDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "OneTimePickupDate", c => c.String());
            AddColumn("dbo.Clients", "PickupDate", c => c.String());
            DropColumn("dbo.Clients", "Address");
            DropColumn("dbo.Clients", "State");
            DropColumn("dbo.Clients", "ZipCode");
            DropColumn("dbo.Clients", "OneTimePickupDay");
            DropColumn("dbo.Clients", "PickupDay");
            DropTable("dbo.PickupDays");
        }
    }
}
