using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PageSitesWeb.Startup))]
namespace PageSitesWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
