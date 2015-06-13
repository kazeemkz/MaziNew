namespace eLibrary.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using eLibrary.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<eLibrary.DAL.eLContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }


        protected override void Seed(eLContext context)
        {

            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");
            }

            if (!Roles.RoleExists("SuperAdmin"))
            {
                Roles.CreateRole("SuperAdmin");
            }
            if (!Roles.RoleExists("Student"))
            {
                Roles.CreateRole("Student");
            }




            if (Membership.GetUser("kazeemkz") == null)
            {
                Membership.CreateUser("kazeemkz", "oyeyemi", "kazeemkz@yahoo.com");
                Roles.AddUserToRole("kazeemkz", "SuperAdmin");
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        //    if (!Roles.RoleExists("Admin"))
        //    {
        //        Roles.CreateRole("Admin");
        //    }

        //    if (!Roles.RoleExists("User"))
        //    {
        //        Roles.CreateRole("User");
        //    }




        //    if (Membership.GetUser("kazeemkz") == null)
        //    {
        //        Membership.CreateUser("kazeemkz", "oyeyemi", "kazeemkz@yahoo.com");
        //        Roles.AddUserToRole("kazeemkz", "Admin");
        //    }
        //    context.Cards.AddOrUpdate(d => d.CardNumber, new Card() { CardNumber = "12345678909" },
        //    new Card { CardNumber = "8901234456" },
        //    new Card { CardNumber = "8901234456" },
        //    new Card { CardNumber = "8901234426" },
        //    new Card { CardNumber = "8901234656" });

        //    context.Subscriptions.AddOrUpdate(d => d.StartDate, new Subscription { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), SubNature = SubscriptionStatus.Active, UserName = "wasiu", LoadCard = "1234568900" });
        //    context.JHUsers.AddOrUpdate(d => d.UserName, new JHUser() { Email = "wasiuabioye@gmail.com", UserName = "wasiu", AreaOfInterest = "Computer Programming", EnableAlert = false, MobileNumber = "08008320847" });
        //    Membership.CreateUser("wasiu", "wasiuabioye", "wasiu@yahoo.com");
        //    Roles.AddUserToRole("wasiu", "User");
        }
    }
}
