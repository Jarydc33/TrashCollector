namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SuspensionDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "SuspensionStart", c => c.String());
            AddColumn("dbo.Clients", "SuspensionEnd", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "SuspensionEnd");
            DropColumn("dbo.Clients", "SuspensionStart");
        }
    }
}
