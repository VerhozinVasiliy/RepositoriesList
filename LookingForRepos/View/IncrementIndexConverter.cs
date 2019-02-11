using System;
using System.Globalization;
using System.Windows.Data;

namespace LookingForRepos.View
{
    [ValueConversion(typeof(int), typeof(int))]
    public class IncrementIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && int.TryParse(value.ToString(), out int v))
            {
                return v + 1;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}
