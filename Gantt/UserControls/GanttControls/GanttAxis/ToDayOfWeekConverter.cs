using Gantt.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Gantt.UserControls.GanttControls.GanttAxis
{
    public class ToDayOfWeekConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int h = 0;
            cOs temp = value as cOs;
            if (temp.Dat.DayOfWeek == DayOfWeek.Monday)
                return temp.Dat.ToString("dd.MM");
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
