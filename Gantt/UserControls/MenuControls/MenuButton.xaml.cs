using Gantt.Model;
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

namespace Gantt.UserControls.MenuControls
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
            DependencyProperty.Register("ImgPath", typeof(String),
              typeof(MenuButton), new PropertyMetadata("/Resources/Img/arrowBack.png"));


        public PossiblePlanSections Choice
        {
            get
            {
                return (PossiblePlanSections)GetValue(ChoiceProperty);
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
            DependencyProperty.Register("Choice", typeof(PossiblePlanSections),
              typeof(MenuButton), new PropertyMetadata(PossiblePlanSections.PurposeAndScope));

        public PossiblePlanSections Number
        {
            get
            {

                return (PossiblePlanSections)GetValue(NumberProperty);
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
            DependencyProperty.Register("Number", typeof(PossiblePlanSections),
              typeof(MenuButton), new PropertyMetadata(PossiblePlanSections.PurposeAndScope));

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
