using System;
using System.Globalization;
using System.Web;
using Xamarin.Forms;

namespace YTCaptions.Converters
{
    public class TextEncodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return HttpUtility.HtmlDecode((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
