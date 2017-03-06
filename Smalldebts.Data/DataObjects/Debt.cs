using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smalldebts.Backend.DataObjects
{
#if MOBILE_SERVICE
    public class Debt : Microsoft.Azure.Mobile.Server.EntityData
#else
    public class Debt : HandmadeEntityData
#endif
    {
        public Debt()
        {
            Movements = new List<Movement>();
        }
        public virtual ICollection<Movement> Movements { get; set; }

        public string Name { get; set; }
        public decimal Balance { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}