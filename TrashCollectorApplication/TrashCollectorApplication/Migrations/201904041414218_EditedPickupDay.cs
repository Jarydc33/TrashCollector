namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedPickupDay : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PickupDays", "GarbagePickupDay", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PickupDays", "GarbagePickupDay", c => c.Int(nullable: false));
        }
    }
}
