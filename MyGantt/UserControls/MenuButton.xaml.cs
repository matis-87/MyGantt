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
    /// Logika interakcji dla klasy MenuButton.xaml
    /// </summary>
    public partial class MenuButton : UserControl
    {
        public String Label
        {
            get { return (String)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string),
              typeof(MenuButton), new PropertyMetadata(""));


        public String ImgPath
        {
            get { return (String)GetValue(ImgPathProperty); }
            set { SetValue(ImgPathProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty ImgPathProperty =
            DependencyProperty.Register("ImgPath", typeof(string),
              typeof(MenuButton), new PropertyMetadata(""));


        public int Choice
        {
            get
            {
                return (int)GetValue(ChoiceProperty);
            }
            set
            {
                SetValue(ChoiceProperty, value);
            }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty ChoiceProperty =
            DependencyProperty.Register("Choice", typeof(int),
              typeof(MenuButton), new PropertyMetadata(0));

        public int Number
        {
            get
            {

                return (int)GetValue(NumberProperty);
            }
            set
            {

                SetValue(NumberProperty, value);
            }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(int),
              typeof(MenuButton), new PropertyMetadata(0));

        public bool isActive
        {
            get { return (bool)GetValue(isActiveProperty); }
            set { SetValue(isActiveProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty isActiveProperty =
            DependencyProperty.Register("isActive", typeof(bool),
              typeof(MenuButton), new PropertyMetadata(true));


        public ICommand MyCommand
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("MyCommand", typeof(ICommand),
              typeof(MenuButton), new PropertyMetadata(null));



        public MenuButton()
        {
            isActive = false;
            InitializeComponent();
            //  this.DataContext = this;

        }
        private void ChangeCurrentButtonNumer()
        {
            Choice = Number;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeCurrentButtonNumer();
            MyCommand?.Execute(Number);
        }
    }
}
