using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CustomerRegistry.Utils
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool) value;
            return boolValue ? Visibility.Visible : (parameter ?? Visibility.Hidden);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Visibility) value) == Visibility.Visible;
        }
    }
}
