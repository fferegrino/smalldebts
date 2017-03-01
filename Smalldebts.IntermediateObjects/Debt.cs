using System;

namespace Smalldebts.ItermediateObjects
{
    public class Debt
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Reason { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}