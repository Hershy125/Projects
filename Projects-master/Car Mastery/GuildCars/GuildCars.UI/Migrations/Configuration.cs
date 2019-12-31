namespace GuildCars.UI.Migrations
{
    using GuildCars.UI.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCars.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCars.UI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleMgr = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            if (roleMgr.RoleExists("admin"))
            {
                return;
            }

            roleMgr.Create(new ApplicationRole() { Name = "admin" });

            if (roleMgr.RoleExists("sales"))
            {
                return;
            }

            roleMgr.Create(new ApplicationRole() { Name = "sales" });

            var user = new ApplicationUser()
            {
                UserName = "testAdmin@guildcars.com",
                FirstName = "Tester",
                LastName = "Person",
                Email = "testAdmin@guildcars.com"

            };

            userMgr.Create(user, "Testing123!");
            userMgr.AddToRole(user.Id, "admin");

        }
    }
}
