using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smalldebts.Backend.ExposedModels
{
    public class NewDebt
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}