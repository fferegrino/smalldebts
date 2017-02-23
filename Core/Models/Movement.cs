using System;
namespace Smalldebts.Core.Models
{
	public class Movement
	{
		public decimal Amount { get; set; }
		public DateTimeOffset Date { get; set; }
	}
}
