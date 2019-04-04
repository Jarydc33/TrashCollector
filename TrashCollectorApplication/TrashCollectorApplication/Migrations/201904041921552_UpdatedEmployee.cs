namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "PickupComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "ZipCode", c => c.String());
            DropColumn("dbo.Employees", "UserName");
            DropColumn("dbo.Employees", "Password");
            DropColumn("dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "EmployeeId", c => c.String());
            AddColumn("dbo.Employees", "Password", c => c.String());
            AddColumn("dbo.Employees", "UserName", c => c.String());
            DropColumn("dbo.Employees", "ZipCode");
            DropColumn("dbo.Clients", "PickupComplete");
        }
    }
}
