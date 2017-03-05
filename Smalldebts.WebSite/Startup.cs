using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Smalldebts.WebSite.Startup))]
namespace Smalldebts.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
