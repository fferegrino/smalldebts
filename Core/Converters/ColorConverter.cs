using System;
using System.Globalization;
using Xamarin.Forms;

namespace Smalldebts.Core.UI.Converters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valor = (decimal) value;
            if (valor < 0) return App.RealCurrent.NegativeColor;
            if (valor > 0) return App.RealCurrent.PositiveColor;
            return App.RealCurrent.NeutralColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}