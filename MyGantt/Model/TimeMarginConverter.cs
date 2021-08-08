using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MyGantt.Model
{
    class TimeMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            int _width = 0;
            try
            {
                DateTime param = System.Convert.ToDateTime(value[1]);
                DateTime start = System.Convert.ToDateTime(value[0]);

                _width = System.Convert.ToInt32(start.Subtract(param).TotalDays) * 40;
                //string ret = 100;
                return new Thickness(_width, 0, 0, 0);
            }
            catch
            {
                return 0;
            }
        }



        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
