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

//using Smalldebts.IntermediateObjects;

namespace Smalldebts.Backend.Controllers.Api
{
    [MobileAppController]
    public class AccountsController : BaseApiAuthController
    {
        //[HttpGet]
        //[Route("confirm")]
        //[AllowAnonymous]
        //public async Task<string> ConfirmEmail()
        //{
        //    var user = await AppUserManager.FindByEmailAsync("demo@messier16.com");

        //    
        //    var callbackUrl = Url.Link("DefaultWeb", new
        //    {
        //        Controller = "Account",
        //        Action = "ConfirmEmail",
        //        userId = user.Id,
        //        code = code
        //    });

        //    return callbackUrl;
        //}


        //[HttpGet]
        //[Route("forgotten")]
        //[AllowAnonymous]
        //public async Task<string> ForgottenPassword()
        //{
        //    var user = await AppUserManager.FindByEmailAsync("demo@messier16.com");

        //    var code = await AppUserManager.GeneratePasswordResetTokenAsync(user.Id);

        //    var callbackUrl = Url.Link("DefaultWeb", new
        //    {
        //        Controller = "Account",
        //        Action = "ResetPassword",
        //        userId = user.Id,
        //        code = code
        //    });

        //    return callbackUrl;
        //}

        [AllowAnonymous]
        [ResponseType(typeof(SimpleUser))]
        public async Task<IHttpActionResult> Post(AccountModelBinding createUserModel)
        {
            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                //FirstName = createUserModel.FirstName,
                //LastName = createUserModel.LastName,
                //Level = 3,
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


            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("smalldebts@messier16.com", "The Smalldebts team"),
                Subject = "Welcome to Smalldebts!",
                PlainTextContent = "Please confirm your account by clicking this link:" + callbackUrl,
                HtmlContent = "Please confirm your account by clicking this link: <a href=\""
                                                   + callbackUrl + "\">link</a>"
            };
            msg.AddTo(new EmailAddress(createUserModel.Username, createUserModel.Username));
            var response = await client.SendEmailAsync(msg);


            return Created(callbackUrl, TheModelFactory.Create(user));
        }
    }
}