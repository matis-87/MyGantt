using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyGantt.Model
{
   public class DayToPresentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime ToDay = DateTime.Now;
           
            try
            {
                cOs temp = value as cOs;
                var d = temp.Dat;
                if ((ToDay.Year == d.Year) && (ToDay.Month == d.Month) && (ToDay.Day == d.Day))
                    return true;
                else return false;
            }
            catch
            {
                return false;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
