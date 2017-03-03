using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Mobile.Server.Config;
using Smalldebts.IntermediateObjects;
using ApplicationUser = Smalldebts.Backend.DataObjects.ApplicationUser;

//using Smalldebts.IntermediateObjects;

namespace Smalldebts.Backend.Controllers
{
    [MobileAppController]
    public class AccountsController : BaseApiController
    {
        [HttpGet]
        [Route("confirm")]
        [AllowAnonymous]
        public async Task<string> ConfirmEmail()
        {
            var user = await AppUserManager.FindByEmailAsync("demo@messier16.com");

            var code = await AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Link("DefaultWeb", new
            {
                Controller = "Account",
                Action = "ConfirmEmail",
                userId = user.Id,
                code = code
            });

            return callbackUrl;
        }

        [Route("me")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(SimpleUser))]
        public async Task<IHttpActionResult> Me()
        {
            var userId = User.Identity.GetUserId();
            var user = await AppUserManager.FindByIdAsync(userId);
            return Ok(TheModelFactory.Create(user));
        }

        [HttpGet]
        [Route("forgotten")]
        [AllowAnonymous]
        public async Task<string> ForgottenPassword()
        {
            var user = await AppUserManager.FindByEmailAsync("demo@messier16.com");

            var code = await AppUserManager.GeneratePasswordResetTokenAsync(user.Id);

            var callbackUrl = Url.Link("DefaultWeb", new
            {
                Controller = "Account",
                Action = "ResetPassword",
                userId = user.Id,
                code = code
            });

            return callbackUrl;
        }
		
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

            return Created("http://smalldebts", TheModelFactory.Create(user));
        }
    }
}