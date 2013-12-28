using SportsTech.Data;
using SportsTech.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportsTech.Web
{
    public class DatabaseConfig
    {
        public static void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());

            // make sure the database is created before SimpleMembership is initialised
            using (var db = new DataContext())
                db.Database.Initialize(true);
        }
    }
}