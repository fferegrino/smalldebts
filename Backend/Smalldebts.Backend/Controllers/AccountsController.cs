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

        //[Route("create")]
        [AllowAnonymous]
        //[ResponseType(typeof(UserReturnModel))]
        public async Task<UserReturnModel> Post(AccountModelBinding createUserModel)
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

            //if (!addUserResult.Succeeded)
            //{
            //    return GetErrorResult(addUserResult);
            //}

            //Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return  TheModelFactory.Create(user);
            System.Diagnostics.Debug.WriteLine(createUserModel);
        }
    }
}