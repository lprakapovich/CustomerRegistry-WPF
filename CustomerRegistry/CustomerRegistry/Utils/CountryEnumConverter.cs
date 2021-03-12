using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using CustomerRegistry.Model;

namespace CustomerRegistry.Utils
{
    public class CountryEnumConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Country country)
            {
                return country.ToString();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string country)
            {
                return Enum.Parse(typeof(Country), country);
            }

            return null;
        }

        public string[] EnumStrings => GetEnumStrings();

        private string[] GetEnumStrings()
        {
            List<string> countries = new List<string>();

            foreach (var country in Enum.GetValues(typeof(Country)))
            {
                countries.Add(country.ToString());
            }

            return countries.ToArray();
        }
    }
}
