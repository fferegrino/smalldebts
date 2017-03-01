using System;
using System.Collections.Generic;
using System.Text;

namespace Smalldebts.IntermediateObjects
{
    public class SimpleUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTimeOffset JoinDate { get; set; }
    }
}
