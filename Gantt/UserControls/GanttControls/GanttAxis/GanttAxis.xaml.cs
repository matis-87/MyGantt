using Gantt.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gantt.UserControls.GanttControls.GanttAxis
{
    /// <summary>
    /// Logika interakcji dla klasy GanttAxis.xaml
    /// </summary>
    public partial class GanttAxis : UserControl
    {
        public ObservableCollection<cOs> TimeAxis
        {
            get { return (ObservableCollection<cOs>)GetValue(TimeAxisProperty); }
            set { SetValue(TimeAxisProperty, value); }
        }

        public static readonly DependencyProperty TimeAxisProperty =
            DependencyProperty.Register("TimeAxis", typeof(ObservableCollection<cOs>),
              typeof(GanttAxis), new PropertyMetadata());


        public GanttAxis()
        {
            InitializeComponent();
        }
    }
}
