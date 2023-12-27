using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Harvest.Bridge.WebSite.Startup))]
namespace Harvest.Bridge.WebSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
