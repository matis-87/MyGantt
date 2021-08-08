using MyGantt.Model;
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

namespace MyGantt.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public ObservableCollection<cOs> TimeAxis
        {
            get { return (ObservableCollection<cOs>)GetValue(TimeAxisProperty); }
            set { SetValue(TimeAxisProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty TimeAxisProperty =
            DependencyProperty.Register("TimeAxis", typeof(ObservableCollection<cOs>),
              typeof(UserControl1), new PropertyMetadata(null));

    }
}
