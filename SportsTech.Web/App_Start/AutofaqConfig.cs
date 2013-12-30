using Autofac;
using Autofac.Integration.Mvc;
using SportsTech.Data;
using SportsTech.Data.Entity;
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
        }
    }
}