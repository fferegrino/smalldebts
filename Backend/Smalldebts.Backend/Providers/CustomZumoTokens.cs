using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Login;

namespace Smalldebts.Backend.Providers
{
    public class CustomZumoTokenFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private string GetHost()
        {
#if DEBUG
            return Constants.Host;
#else
            return string.Format("https://{0}.azurewebsites.net/",
            Environment.ExpandEnvironmentVariables("%WEBSITE_SITE_NAME%")
                .ToLower());
#endif
        }


        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            // Get Signing Key and send x-zumo-auth token from claims
            string signingKey = GetSigningKey();
            var tokenInfo = AppServiceLoginHandler.CreateToken(
                    data.Identity.Claims,
                    signingKey,
                    GetHost(),
                    GetHost(),
                    TimeSpan.FromHours(24));

            return tokenInfo.RawData;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }

        private string GetSigningKey()
        {
            string key =
                Environment.GetEnvironmentVariable("WEBSITE_AUTH_SIGNING_KEY");

            if (string.IsNullOrWhiteSpace(key))
                key = Constants.SigninKey;

            return key;
        }
    }
}