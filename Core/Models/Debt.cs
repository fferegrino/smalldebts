using System;
namespace Smalldebts.Core.Models
{
	public class Debt
    {
		public string Id { get; set; }
		public string Name { get; set; }
		public decimal Balance { get; set; }
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset ModifiedDate { get; set; }
	}
}
