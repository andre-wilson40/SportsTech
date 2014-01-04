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
            AddUsersAsRequired(context);
            AddRolesAsRequired(context);             
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
                        DateFormat = "dd-mmm-yyyy",
                        EmailAddress = "andre.wilson40@gmail.com",                        
                    }
                };

                userManager.Create(awilson, "aw181979");
                userManager.AddToRole(awilson.Id, Role.Administrator);
                
                var waihou = new Model.Club
                    {
                        Name = "Waihou",
                        Address = "Waihou",
                    };

                awilson.UserProfile.Clubs.Add(waihou);              
                context.SaveChanges();
            }
        }

        private void AddRolesAsRequired(SportsTech.Data.Entity.DataContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Add Roles
            if (!roleManager.RoleExists(Role.User)) roleManager.Create(new IdentityRole(Role.User));
            if (!roleManager.RoleExists(Role.Administrator)) roleManager.Create(new IdentityRole(Role.Administrator));

             
        }
    }
}
