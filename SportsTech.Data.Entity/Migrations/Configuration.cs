namespace SportsTech.Data.Entity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
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
            AddClubsAsRequired(context);
        }

        private void AddClubsAsRequired(SportsTech.Data.Entity.DataContext context)
        {
            AddRolesAsRequired(context);

            //// If we have been here before don't go any further
            //if (context.Clubs.Any(p => p.Name == "Waihou")) return;

            //context.Clubs.AddOrUpdate(
            //    p => p.Name,
            //    new Model.Club
            //    {
            //        Name = "Waihou",
            //        Address = "Waihou",
            //    });
            
        }

        //private void AddUsersAsRequired(SportsTech.Web.Models.ApplicationDbContext context)
        //{
        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        //    if (userManager.FindByName("awilson") == null)
        //    {
        //        var registration = new RegisterViewModel
        //        {
        //            EmailAddress = "andre.wilson40@gmail.com",
        //            FirstName = "Andre",
        //            LastName = "Wilson",
        //            UserName = "awilson"
        //        };

        //        var awilson = Mapper.Map<ApplicationUser>(registration);
        //        awilson.Roles.Add(new IdentityUserRole() { Role = new IdentityRole(Role.Administrator) });
        //        userManager.Create(awilson);
        //    }
        //}

        private void AddRolesAsRequired(SportsTech.Data.Entity.DataContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Add Roles
            if (!roleManager.RoleExists(Role.User)) roleManager.Create(new IdentityRole(Role.User));
            if (!roleManager.RoleExists(Role.Administrator)) roleManager.Create(new IdentityRole(Role.Administrator));
        }
    }
}
