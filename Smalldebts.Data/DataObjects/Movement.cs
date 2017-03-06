using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smalldebts.Backend.DataObjects
{
#if MOBILE_SERVICE
    public class Movement : Microsoft.Azure.Mobile.Server.EntityData
#else
    public class Movement : HandmadeEntityData
#endif
    {
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset Date { get; set; }

        public string DebtId { get; set; }

        [ForeignKey("DebtId")]
        public virtual Debt Debt { get; set; }
    }
}