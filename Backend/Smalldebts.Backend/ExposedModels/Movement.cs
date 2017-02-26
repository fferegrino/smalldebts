using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smalldebts.Backend.ExposedModels
{
    public class Movement
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public string DebtId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}