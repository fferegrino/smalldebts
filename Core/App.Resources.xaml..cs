using Smalldebts.Core.UI.Converters;
using Xamarin.Forms;

namespace Smalldebts.Core.UI
{
    public partial class App : Application
    {
        public Color NegativeColor => (Color) Resources["NegativeColor"];
        public Color PositiveColor => (Color) Resources["PositiveColor"];
        public Color NeutralColor => (Color) Resources["NeutralColor"];

        public ColorConverter ColorConverter => (ColorConverter) Resources["ColorConverter"];

        private void SetupCodedStyles()
        {
            var largeFontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));


            var notSoYugeLabel = new Style(typeof(Label));

            notSoYugeLabel.Setters.Add(Label.FontSizeProperty, largeFontSize * 1.3);
            notSoYugeLabel.Setters.Add(Label.HorizontalTextAlignmentProperty, TextAlignment.Center);
            notSoYugeLabel.Setters.Add(View.MarginProperty, new Thickness(5, 10));


            var yugeLabelStyle = new Style(typeof(Label)) {BasedOn = notSoYugeLabel};
            yugeLabelStyle.Setters.Add(Label.FontSizeProperty, largeFontSize * 2);

            Resources.Add("YugeLabel", yugeLabelStyle);
            Resources.Add("NotSoYugeLabel", notSoYugeLabel);
        }
    }
}