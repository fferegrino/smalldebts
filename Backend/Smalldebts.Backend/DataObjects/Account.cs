using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace Smalldebts.Backend.DataObjects
{
    public class Account : EntityData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] Salt { get; set; }
        public byte[] SaltedAndHashedPassword { get; set; }
    }
}