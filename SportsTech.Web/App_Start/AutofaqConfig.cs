using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsTech.Data;
using SportsTech.Data.Entity;
using SportsTech.Domain.Services;
using SportsTech.Domain.Services.Core;
using SportsTech.Web.Controllers;
using SportsTech.Web.Models;
using SportsTech.Web.Services;
using SportsTech.Web.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            RegisterDependencies(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterDependencies(ContainerBuilder builder) 
        {
            RegisterHttpDependencies(builder);
            RegisterDatabaseServices(builder);
            RegisterServices(builder);
            RegisterWebServices(builder);
        }

        private static void RegisterHttpDependencies(ContainerBuilder builder)
        {
            builder.Register(c => new HttpContextWrapper(HttpContext.Current))
                .As<HttpContextBase>() 
                .InstancePerHttpRequest();
        }

        private static void RegisterDatabaseServices(ContainerBuilder builder)
        {
            builder.RegisterType<SportsTech.Data.Entity.DataContext>()
                .As<IDataContext>()
                .InstancePerHttpRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<UserStore<ApplicationUser>>()
                   .As<IUserStore<ApplicationUser>>()
                   .WithParameter((pi, ctx) => { return pi.Name == "context"; },
                                  (pi, ctx) => { return ctx.Resolve<IDataContext>(); }
                              );

            builder.RegisterType<UserManager<ApplicationUser>>().As<UserManager<ApplicationUser>>();            
        }

        private static void RegisterWebServices(ContainerBuilder builder)
        {
            builder.RegisterType<ClubSeasonFacadeService>().As<IClubSeasonFacadeService>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<EventService>().As<IEventService>();
            builder.RegisterType<ClubService>().As<IClubService>();
            
            builder.RegisterType<TeamService>().As<ITeamService>()
                       .WithParameter((pi, ctx) => { return pi.Name == "club"; }, ResolveClub);
            
            builder.RegisterType<CompetitionService>().As<ICompetitionService>()
                       .WithParameter((pi, ctx) => { return pi.Name == "club"; }, ResolveClub);

            builder.RegisterType<SeasonServiceFactory>().As<ISeasonServiceFactory>();
        }

        private static Data.Model.Club ResolveClub(System.Reflection.ParameterInfo pi, IComponentContext ctx)
        {
            var clubService = ctx.Resolve<IClubService>();
            var contextWrapper = ctx.Resolve<HttpContextBase>();
            var clubId = Int32.Parse(contextWrapper.Request.RequestContext.RouteData.Values["clubId"].ToString());

            return Library.AsyncHelpers.RunSync<Data.Model.Club>(() => clubService.SingleAsync(p => p.Id == clubId));
        }
    }
}