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

namespace Gantt.UserControls.WBSControls.WBSEditBox
{
    /// <summary>
    /// Logika interakcji dla klasy WBSEditBox.xaml
    /// </summary>
    public partial class WBSEditBox : UserControl
    {
        public WBSEditBox()
        {
            InitializeComponent();
            isReadOnlyModeEnable = true;
            tb.IsReadOnly = true;
        }
        public bool isReadOnlyModeEnable
        {
            get
            {

                return (bool)GetValue(isEditModeEnableProperty);
            }
            set
            {

                SetValue(isEditModeEnableProperty, value);
            }
        }

        public static readonly DependencyProperty isEditModeEnableProperty =
    DependencyProperty.Register("isEditModeEnable", typeof(bool),
      typeof(WBSEditBox), new PropertyMetadata(false));


        public string Text
        {
            get
            {

                return (string)GetValue(TextProperty);
            }
            set
            {

                SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty =
    DependencyProperty.Register("Text", typeof(string),
      typeof(WBSEditBox), new PropertyMetadata(null));





        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           isReadOnlyModeEnable = false;
            tb.IsReadOnly = false;
      
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            isReadOnlyModeEnable = true;
            tb.IsReadOnly = true;
        }
    }
}
