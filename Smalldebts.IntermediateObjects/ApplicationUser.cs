﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Smalldebts.IntermediateObjects
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public DateTimeOffset JoinDate { get; set; }
        public string Email { get; set; }
    }
}
