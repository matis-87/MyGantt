using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjectsMenagament.Resources.Helpers
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DateTime tempDate = DateTime.Parse(value as String);
                return new DateTime(tempDate.Year, tempDate.Month, tempDate.Day);
            }
            catch
            {
                DateTime today = DateTime.Now;
                return new DateTime(today.Year, today.Month, today.Day);
            }
            
        }
    }
}
