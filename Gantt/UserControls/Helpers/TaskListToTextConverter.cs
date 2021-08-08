using Gantt.Model;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Gantt.UserControls.Helpers
{
    class TaskListToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<ProjectTask> Predecessors = value as ObservableCollection<ProjectTask>;
            StringBuilder StringOfWBSCodes = new StringBuilder();
            foreach(ProjectTask Predecessor in Predecessors)
            {
                StringOfWBSCodes.Append(Predecessor.WbsCode + ";");
            }
            return StringOfWBSCodes.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
