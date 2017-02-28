﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Smalldebts.Backend.DataObjects;
using Smalldebts.Backend.Models;

namespace Smalldebts.Backend.Providers
{
    public class CustomAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(
                        OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in
                        context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key,
                                                         property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(
                            OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add(
                        "Access-Control-Allow-Origin",
                        new[] { allowedOrigin });

            var userManager = context.OwinContext
                                     .GetUserManager<ApplicationUserManager>();

            ApplicationUser user = await userManager.FindAsync(context.UserName,
                                                              context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant",
                                 "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity =
                await user.GenerateUserIdentityAsync(userManager, "JWT");

            var props = new AuthenticationProperties(
                new Dictionary<string, string> { { "guid", user.Id } });
            var ticket = new AuthenticationTicket(oAuthIdentity, props);

            context.Validated(ticket);
        }
    }
}