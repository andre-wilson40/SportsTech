using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportsTech.Web.Startup))]
namespace SportsTech.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
