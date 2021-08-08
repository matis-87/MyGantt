using Gantt.Model;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Gantt.UserControls.GanttControls.GanttChart
{
    class DayToTaskStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ProjectTask temp = value as ProjectTask;
            DateTime Today = DateTime.Now;
            if (Today.CompareTo(temp.Start) > 0)
                return 1;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
