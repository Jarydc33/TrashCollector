namespace TrashCollectorApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "UsersFK", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clients", "UsersFK");
            AddForeignKey("dbo.Clients", "UsersFK", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "UsersFK", "dbo.AspNetUsers");
            DropIndex("dbo.Clients", new[] { "UsersFK" });
            DropColumn("dbo.Clients", "UsersFK");
        }
    }
}
