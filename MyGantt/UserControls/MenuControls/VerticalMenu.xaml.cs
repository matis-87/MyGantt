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

namespace MyGantt.UserControls.MenuControls
{
    /// <summary>
    /// Logika interakcji dla klasy VerticalMenu.xaml
    /// </summary>
    public partial class VerticalMenu : UserControl
    {
        public int CurrentNumber
        {
            get
            {

                return (int)GetValue(CurrentNumberProperty);
            }
            set
            {

                SetValue(CurrentNumberProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentNumberProperty =
    DependencyProperty.Register("CurrentNumber", typeof(int),
      typeof(VerticalMenu), new PropertyMetadata(0));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand),
              typeof(VerticalMenu), new PropertyMetadata(null));

        public VerticalMenu()
        {
            InitializeComponent();
        }
    }
}
