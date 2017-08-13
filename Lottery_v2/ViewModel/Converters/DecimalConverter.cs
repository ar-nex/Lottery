using System;
using System.Windows.Data;

namespace Lottery_v2.ViewModel.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal val = (decimal)value;
            return (val == 0) ? string.Empty : val.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strVal = (string)value;
            decimal ret = 0;
            if (string.IsNullOrEmpty(strVal))
            {
                return ret;
            }
            else
            {
                Decimal.TryParse(strVal, out ret);
                return ret;
            }
        }
    }
}
