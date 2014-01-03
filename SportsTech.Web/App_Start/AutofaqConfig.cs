using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsTech.Data;
using SportsTech.Data.Entity;
using SportsTech.Domain.Services;
using SportsTech.Domain.Services.Core;
using SportsTech.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web
{
    public class AutofaqConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            RegisterDependencies(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterDependencies(ContainerBuilder builder) 
        {
            builder.RegisterType<SportsTech.Data.Entity.DataContext>().As<IDataContext>().InstancePerHttpRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser>>();
            builder.RegisterType<UserManager<ApplicationUser>>().As<UserManager<ApplicationUser>>();            
            builder.RegisterType<BaseAuthenticatedController>().PropertiesAutowired();
            
            RegisterServices(builder);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<EventService>().As<IEventService>();
            builder.RegisterType<ClubService>().As<IClubService>();
        }
    }
}