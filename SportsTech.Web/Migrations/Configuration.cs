namespace SportsTech.Web.Migrations
{
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportsTech.Data;
    using SportsTech.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SportsTech.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SportsTech.Web.Models.ApplicationDbContext context)
        {
            AddRolesAsRequired(context);
            AddUsersAsRequired(context);
        }

        private void AddRolesAsRequired(SportsTech.Web.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Add Roles
            if (!roleManager.RoleExists(Role.User)) roleManager.Create(new IdentityRole(Role.User));
            if (!roleManager.RoleExists(Role.Administrator)) roleManager.Create(new IdentityRole(Role.Administrator));
        }

        private void AddUsersAsRequired(SportsTech.Web.Models.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (userManager.FindByName("awilson") == null)
            {
                var registration = new RegisterViewModel
                {
                    EmailAddress = "andre.wilson40@gmail.com",
                    FirstName = "Andre",
                    LastName = "Wilson",
                    UserName = "awilson"
                };

                var awilson = Mapper.Map<ApplicationUser>(registration);
                awilson.Roles.Add(new IdentityUserRole() { Role = new IdentityRole(Role.Administrator) });
                userManager.Create(awilson);
            }
        }
    }
}
