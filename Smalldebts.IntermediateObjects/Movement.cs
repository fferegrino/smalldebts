using System;

namespace Smalldebts.ItermediateObjects
{
    public class Movement
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}