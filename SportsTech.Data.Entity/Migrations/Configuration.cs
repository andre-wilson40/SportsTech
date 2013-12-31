namespace SportsTech.Data.Entity.Migrations
{
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
            if (!context.Clubs.Any(p => p.Name == "Waihou"))
            {
                context.Clubs.Add(new Model.Club
                {
                    Name = "Waihou",
                    Address = "Waihou",                    
                });
            }
        }
    }
}
