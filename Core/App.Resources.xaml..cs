using System;
using System.Collections.Generic;
using Smalldebts.Core.UI.Views;
using Xamarin.Forms;

namespace Smalldebts.Core.UI
{
	public partial class App : Application
	{

		void SetupCodedStyles()
		{
			var yugeLabelStyle = new Style(typeof(Label));
			yugeLabelStyle.Setters.Add(Label.FontSizeProperty,
									   Device.GetNamedSize(NamedSize.Large, typeof(Label)) * 2);
			Resources.Add("YugeLabel", yugeLabelStyle);
		}

		public Color NegativeColor => (Color)Resources["NegativeColor"];
		public Color PositiveColor => (Color)Resources["PositiveColor"];
		public Color NeutralColor => (Color)Resources["NeutralColor"];
	}
}
