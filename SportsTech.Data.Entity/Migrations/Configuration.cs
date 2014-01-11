namespace SportsTech.Data.Entity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SportsTech.Data.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    public class Configuration : DbMigrationsConfiguration<SportsTech.Data.Entity.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SportsTech.Data.Entity.DataContext context)
        {
            AddRolesAsRequired(context);        
            AddUsersAsRequired(context);     
        }

        private void AddUsersAsRequired(SportsTech.Data.Entity.DataContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (userManager.FindByName("awilson") == null)
            {
                var awilson = new ApplicationUser
                {
                    UserName = "awilson",
                    UserProfile = new Model.UserProfile
                    {
                        FirstName = "Andre",
                        LastName = "Wilson",
                        TimeZone = "en-NZ",
                        DateFormat = "dd-MMM-yyyy",
                        EmailAddress = "andre.wilson40@gmail.com",                        
                    }
                };

                var waihou = new Model.Club
                    {
                        Name = "Waihou",
                        Address = "Waihou",
                    };

                awilson.UserProfile.Clubs.Add(waihou);

                userManager.Create(awilson, "aw181979");
                userManager.AddToRole(awilson.Id, Role.Administrator);                
            }
        }

        private void AddRolesAsRequired(SportsTech.Data.Entity.DataContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Add Roles
            if (!roleManager.RoleExists(Role.User)) roleManager.Create(new IdentityRole(Role.User));
            if (!roleManager.RoleExists(Role.Administrator)) roleManager.Create(new IdentityRole(Role.Administrator));
            if (!roleManager.RoleExists(Role.ClubManager)) roleManager.Create(new IdentityRole(Role.ClubManager));
        }
    }
}
