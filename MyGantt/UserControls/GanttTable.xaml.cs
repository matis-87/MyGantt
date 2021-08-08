using MyGantt.Model;
using System;
using System.Collections.Generic;
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

namespace MyGantt.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy GanttTable.xaml
    /// </summary>
    public partial class GanttTable : UserControl
    {
        public object ItemsSource
        {
            get
            {

                return (object)GetValue(ItemsSourceProperty);
            }
            set
            {

                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
    DependencyProperty.Register("ItemsSource", typeof(object),
      typeof(GanttTable), new PropertyMetadata(null));

        public GantTask SelectedTask
        {
            get
            {

                return (GantTask)GetValue(SelectedTaskProperty);
            }
            set
            {

                SetValue(SelectedTaskProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedTaskProperty =
    DependencyProperty.Register("SelectedTask", typeof(GantTask),
      typeof(GanttTable), new PropertyMetadata(null));

        public GanttTable()
        {
            InitializeComponent();
        }
        GantTask LastSelectedTask;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedTask != null)
            {
                if (LastSelectedTask != null)
                {
                    if (!SelectedTask.Equals(LastSelectedTask))
                    {
                        LastSelectedTask.IsSelected = false;
                    }
                    LastSelectedTask.IsSelected = false;
                }
                SelectedTask.IsSelected = true;
                LastSelectedTask = SelectedTask;

            }
        }
    }
}
