﻿using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Mobile.Server.Config;
using Smalldebts.IntermediateObjects;

namespace Smalldebts.Backend.Controllers.Api
{
    [MobileAppController]
    public class MeController : BaseApiAuthController
    {
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(SimpleUser))]
        public async Task<IHttpActionResult> Me()
        {
            var userId = User.Identity.GetUserId();
            var user = await AppUserManager.FindByIdAsync(userId);
            return Ok(TheModelFactory.Create(user));
        }
    }
}
