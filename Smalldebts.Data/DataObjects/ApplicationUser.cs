using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Smalldebts.Backend.DataObjects
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            Debts = new List<Debt>();
        }

        [Required]
        public DateTimeOffset JoinDate { get; set; }
        public virtual ICollection<Debt> Debts { get; set; }


#if MOBILE_SERVICE
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
        UserManager<ApplicationUser> manager,
        string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                                            authenticationType);
            userIdentity.AddClaim(new Claim(System.IdentityModel.Tokens.JwtRegisteredClaimNames.Sub,
                                            this.UserName));
            return userIdentity;
        }
#else
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
#endif



    }
}