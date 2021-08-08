using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyGantt.Model
{
    public class ToWeekOfYearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            cOs temp = value as cOs;
            CultureInfo myCI = new CultureInfo("pl-PL");
            Calendar cal = myCI.Calendar;
            if (temp.Dat.DayOfWeek == DayOfWeek.Wednesday)
                return cal.GetWeekOfYear(temp.Dat, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday).ToString();
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
