namespace Craft.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Craft.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Craft.Models.ApplicationDbContext context)
        {
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
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));

            if (!roleManager.RoleExists("Customer"))
                roleManager.Create(new IdentityRole("Customer"));


            if (!roleManager.RoleExists("Distributor"))
                roleManager.Create(new IdentityRole("Distributor"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (userManager.FindByEmail("a@a.a") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "a@a.a",
                    UserName = "a@a.a",
                };
                var result = userManager.Create(user, "Password");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Admin");
            }
            if (userManager.FindByEmail("c@c.c") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "c@c.c",
                    UserName = "c@c.c",
                };
                var result = userManager.Create(user, "Password");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Customer");
            }
            if (userManager.FindByEmail("e@e.e") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "d@d.d",
                    UserName = "d@d.d",
                };
                var result = userManager.Create(user, "Password");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByEmail(user.Email).Id, "Distributor");
            }
        }
    }
}
