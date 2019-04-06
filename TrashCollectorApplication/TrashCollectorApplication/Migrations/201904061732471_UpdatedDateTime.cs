namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "SuspensionStartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clients", "SuspensionEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clients", "OneTimePickupDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Clients", "SuspensionStart");
            DropColumn("dbo.Clients", "SuspensionEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "SuspensionEnd", c => c.String());
            AddColumn("dbo.Clients", "SuspensionStart", c => c.String());
            DropColumn("dbo.Clients", "OneTimePickupDate");
            DropColumn("dbo.Clients", "SuspensionEndDate");
            DropColumn("dbo.Clients", "SuspensionStartDate");
        }
    }
}
