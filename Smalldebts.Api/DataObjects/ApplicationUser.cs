using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
        UserManager<ApplicationUser> manager,
        string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                                            authenticationType);
            userIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                                            this.UserName));
            return userIdentity;
        }
    }
}