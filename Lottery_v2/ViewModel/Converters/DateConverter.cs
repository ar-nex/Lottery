using System;
using System.Globalization;
using System.Windows.Data;

namespace Lottery_v2.ViewModel.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dt = (DateTime)value;
            if (dt.Year == 1)
                return string.Empty;
            else
                return dt.ToString("dd-MM-yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dt = new DateTime();
            string dt_str = (string)value;
            string strr = string.Empty;
            if (!string.IsNullOrEmpty(dt_str))
            {
                if (dt_str.IndexOf('-') > -1)
                {
                    strr = "--";
                    try
                    {
                        DateTime.TryParseExact(dt_str, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                    }
                    catch (Exception)
                    {

                    }
                }
                else if (dt_str.IndexOf('/') > -1)
                {
                    strr = "/";
                    try
                    {
                        DateTime.TryParseExact(dt_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    strr = "-|-";
                    try
                    {
                        dt = DateTime.Parse(dt_str);
                    }
                    catch (Exception)
                    {


                    }
                }

            }
            return dt;
        }
    }
}
