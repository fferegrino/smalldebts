using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Smalldebts.Backend.Startup))]

namespace Smalldebts.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}