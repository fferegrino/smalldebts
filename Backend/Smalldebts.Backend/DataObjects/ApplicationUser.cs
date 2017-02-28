using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
    }
}