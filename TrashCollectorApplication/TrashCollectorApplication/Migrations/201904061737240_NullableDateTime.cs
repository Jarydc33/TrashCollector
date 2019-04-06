namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "SuspensionStartDate", c => c.DateTime());
            AlterColumn("dbo.Clients", "SuspensionEndDate", c => c.DateTime());
            AlterColumn("dbo.Clients", "OneTimePickupDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "OneTimePickupDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Clients", "SuspensionEndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Clients", "SuspensionStartDate", c => c.DateTime(nullable: false));
        }
    }
}
