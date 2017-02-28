using Microsoft.Owin;
using Owin;
using Smalldebts.Backend.Models;

[assembly: OwinStartup(typeof(Smalldebts.Backend.Startup))]

namespace Smalldebts.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.CreatePerOwinContext(MobileServiceContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            ConfigureMobileApp(app);
        }
    }
}