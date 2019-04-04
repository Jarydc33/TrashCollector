namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewClientDatabase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Clients", "UserName");
            DropColumn("dbo.Clients", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Password", c => c.String());
            AddColumn("dbo.Clients", "UserName", c => c.String());
        }
    }
}
