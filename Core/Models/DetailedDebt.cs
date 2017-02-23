using System;
using System.Collections.Generic;

namespace Smalldebts.Core.Models
{
	public class DetailedDebt : Debt
	{
		public List<Movement> Movements { get; set; }
	}
}
