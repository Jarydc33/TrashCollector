namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatLong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Latitude", c => c.Single(nullable: false));
            AddColumn("dbo.Clients", "Longitutde", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Longitutde");
            DropColumn("dbo.Clients", "Latitude");
        }
    }
}
