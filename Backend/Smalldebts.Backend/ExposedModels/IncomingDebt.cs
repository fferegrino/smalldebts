using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smalldebts.Backend.ExposedModels
{
    public class IncomingDebt
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Reason { get; set; }
    }
}