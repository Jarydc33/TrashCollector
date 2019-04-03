namespace TrashCollectorApplication.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TrashCollectorApplication.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TrashCollectorApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TrashCollectorApplication.Models.ApplicationDbContext context)
        {
            //if (!context.Roles.Any(r => r.Name == "Administrator"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "Administrator" };

            //    manager.Create(role);
            //}

            //if (!context.Users.Any(u => u.UserName == "Administrator"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser { UserName = "Administrator" };

            //    manager.Create(user, "ChangeItAsap!");
            //    manager.AddToRole(user.Id, "Administrator");
            //}

            //if (!context.Roles.Any(r => r.Name == "Employee"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "Employee" };

            //    manager.Create(role);
            //}

            //if (!context.Roles.Any(r => r.Name == "Client"))
            //{
            //    var store = new RoleStore<IdentityRole>(context);
            //    var manager = new RoleManager<IdentityRole>(store);
            //    var role = new IdentityRole { Name = "Client" };

            //    manager.Create(role);
            //}
        }
    }
}
