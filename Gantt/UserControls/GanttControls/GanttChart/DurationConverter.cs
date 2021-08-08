using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Gantt.UserControls.GanttControls.GanttChart
{
 public   class DurationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double temp = 0;
            try
            {
                temp = System.Convert.ToDouble(values[0]);
            }
            catch
            {
                return 0;
            }
            return (temp * 40);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
