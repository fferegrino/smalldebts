﻿using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Smalldebts.Backend.DataObjects
{
    public class Debt : EntityData
    {
        public Debt()
        {
            Movements = new List<Movement>();
        }

        public string Name { get; set; }
        public decimal Balance { get; set; }

        public virtual ICollection<Movement> Movements { get; set; }
    }
}