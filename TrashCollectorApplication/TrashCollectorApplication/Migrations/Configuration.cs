namespace TrashCollectorApplication.Migrations
{
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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var Sunday = new PickupDay { GarbagePickupDay = "Sunday" };
            var Monday = new PickupDay { GarbagePickupDay = "Monday" };
            var Tuesday = new PickupDay { GarbagePickupDay = "Tuesday" };
            var Wednesday = new PickupDay { GarbagePickupDay = "Wednesday" };
            var Thursday = new PickupDay { GarbagePickupDay = "Thursday" };
            var Friday = new PickupDay { GarbagePickupDay = "Friday" };
            var Saturday = new PickupDay { GarbagePickupDay = "Saturday" };

            context.PickupDays.AddOrUpdate(
                p => p.GarbagePickupDay,
                Sunday,
                Monday,
                Tuesday,
                Wednesday,
                Thursday,
                Friday,
                Saturday,
                Sunday
                );
        }
    }
}
