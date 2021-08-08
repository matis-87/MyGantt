using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Gantt.UserControls.MenuControls
{
    public class CurrentButtonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int CurrentButtonNumber = (int)values[0];
            int ButtonNumber = (int)values[1];
            if (CurrentButtonNumber == ButtonNumber)
                return true;
            else
                return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
