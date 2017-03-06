using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual ICollection<Movement> Movements { get; set; }

        public string Name { get; set; }
        public decimal Balance { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}