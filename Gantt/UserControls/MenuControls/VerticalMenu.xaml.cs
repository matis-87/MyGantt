using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Gantt.Model;
namespace Gantt.UserControls.MenuControls
{


    public partial class VerticalMenu : UserControl
    {
        public PossiblePlanSections CurrentNumber
        {
            get
            {

                return (PossiblePlanSections)GetValue(CurrentNumberProperty);
            }
            set
            {

                SetValue(CurrentNumberProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentNumberProperty =
    DependencyProperty.Register("CurrentNumber", typeof(PossiblePlanSections),
      typeof(VerticalMenu), new PropertyMetadata(PossiblePlanSections.PurposeAndScope));

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
