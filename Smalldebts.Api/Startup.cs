using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Smalldebts.Backend.Models;
using Smalldebts.Backend.Providers;
using Smalldebts.Data;

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
            ConfigureCustomAuth(app);
        }

        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        
        public static void ConfigureCustomAuth(IAppBuilder appBuilder)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions =
                new OAuthAuthorizationServerOptions()
                {
#if DEBUG
                    AllowInsecureHttp = true, // NEVER IN PRODUCTION!
#endif
                    TokenEndpointPath = new PathString("/api/oauth/token"),
                    Provider = new CustomAuthProvider(),
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                    AccessTokenFormat = new CustomZumoTokenFormat(),
                };

            // OAuth Configuration
            DataProtectionProvider = appBuilder.GetDataProtectionProvider();
            appBuilder.UseOAuthAuthorizationServer(oAuthServerOptions);
        }
    }
}