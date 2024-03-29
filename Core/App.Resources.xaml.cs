﻿using Smalldebts.Core.UI.Converters;
using Smalldebts.Core.UI.Resources;
using Smalldebts.Core.UI.Services;
using Xamarin.Forms;

namespace Smalldebts.Core.UI
{
    public partial class App : Application
    {
        public Color NegativeColor => (Color)Resources["NegativeColor"];
        public Color PositiveColor => (Color)Resources["PositiveColor"];
		public Color NegativeWashedColor => (Color)Resources["NegativeWashedColor"];
		public Color PositiveWashedColor => (Color)Resources["PositiveWashedColor"];
		public Color NegativeStrongColor => (Color)Resources["NegativeStrongColor"];
		public Color PositiveStrongColor => (Color)Resources["PositiveStrongColor"];
        public Color NeutralColor => (Color)Resources["NeutralColor"];
        public Color NeutralWashedColor => (Color)Resources["NeutralWashedColor"];
        public Color BrandColor => (Color)Resources["BrandColor"];
		public Color BrandLightColor => (Color)Resources["BrandLightColor"];

        public ColorConverter ColorConverter => (ColorConverter) Resources["ColorConverter"];

        private void SetupCodedStyles()
        {
            var largeFontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));


            var notSoYugeLabel = new Style(typeof(Label));

            notSoYugeLabel.Setters.Add(Label.FontSizeProperty, largeFontSize * 1.3);
            notSoYugeLabel.Setters.Add(Label.HorizontalTextAlignmentProperty, TextAlignment.Center);
            notSoYugeLabel.Setters.Add(View.MarginProperty, new Thickness(5, 10));


            var yugeLabelStyle = new Style(typeof(Label)) {BasedOn = notSoYugeLabel};
            yugeLabelStyle.Setters.Add(Label.FontSizeProperty, largeFontSize * 1.5);

            Resources.Add("YugeLabel", yugeLabelStyle);
            Resources.Add("NotSoYugeLabel", notSoYugeLabel);


        }

        private void SetupLanguage()
        {

            if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
            {
                var ci = DependencyService.Get<ILocalization>().GetCurrentCultureInfo();
                AppStrings.Culture = ci;
                DependencyService.Get<ILocalization>().SetLocale(ci); 
            }
        }
    }
}