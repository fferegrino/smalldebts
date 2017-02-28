using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Smalldebts.IntermediateObjects
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        [Required]
        public DateTimeOffset JoinDate { get; set; }
        public string Email { get; set; }
    }
}
