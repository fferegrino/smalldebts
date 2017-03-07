using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Mobile.Server.Config;
using SendGrid;
using SendGrid.Helpers.Mail;
using Smalldebts.IntermediateObjects;
using ApplicationUser = Smalldebts.Backend.DataObjects.ApplicationUser;
using System.Net;
using Smalldebts.Backend.Providers;

//using Smalldebts.IntermediateObjects;

namespace Smalldebts.Backend.Controllers.Api
{
    [MobileAppController]
    public class AccountsController : BaseApiAuthController
    {
        private IMailProvider _mailProvider;

        public AccountsController(IMailProvider mailProvider)
        {
            _mailProvider = mailProvider;
        }

        [AllowAnonymous]
        [ResponseType(typeof(SimpleUser))]
        public async Task<IHttpActionResult> Post(AccountModelBinding createUserModel)
        {
            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                JoinDate = DateTime.Now.Date,
            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            var code = await AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            string site;
#if DEBUG
            site = Constants.Site;
#else
            site = String.Format("https://{0}.azurewebsites.net/",
            Environment.ExpandEnvironmentVariables("%WEBSITE_SITE_NAME%")
                .ToLower());
#endif
            var callbackUrl = site + $"account/confirmemail?userId={Uri.EscapeDataString(user.Id)}&code={Uri.EscapeDataString(code)}";
            var subject = "Welcome to Smalldebts!";
            var plainTextContent = "Please confirm your account by clicking this link:" + callbackUrl;
            var htmlContent = "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>";


            var emailSent = _mailProvider.SendEmailAsync(
                new Email() {Address = "smalldebts@messier16.com", Name = "Smalldebts team"},
                "Welcome to Smalldebts!",
                plainTextContent,
                htmlContent,
                new Email() {Address = user.Email, Name = user.UserName});

            return Created(callbackUrl, TheModelFactory.Create(user));
        }
    }
}