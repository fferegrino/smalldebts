using System;

using Xamarin.Forms;

namespace Smalldebts.Core.UI
{
	public static class NumberExtensions
	{
		public static double LargeSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
		public static double SizeForDigit(this decimal value)
		{
			var v = $"{Math.Abs(value):0}";
			if (v.Length < 2)
				return LargeSize * 4;
			if (v.Length < 4)
				return LargeSize * 3;
			if (v.Length < 6)
				return LargeSize * 2;
			return LargeSize;
		}
	}
}

