using System.Windows;
using System.Windows.Forms;

namespace MyGantt.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            okno = Window.GetWindow(this);
            width = 700;
            height = 500;
            _border.Margin = new Thickness(5);
        }
        Window okno;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Label_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                if (!WinState)
                {
                    _border.Margin = new Thickness(5);
                    Normalize(e.GetPosition(okno));

                }
                this.DragMove();
            }
        }
        double width;
        double height;
        bool WinState = true;
        private void Label_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (!WinState)
            {
                _border.Margin = new Thickness(5);
                Normalize(e.GetPosition(okno));

            }
            else
            if (WinState)
            {
                _border.Margin = new Thickness(0);
                Maximize();


            }
        }

        void Normalize(Point pos)
        {
            Point ScreenPos = PointToScreen(pos);
            Screen _screen = Screen.FromPoint(Control.MousePosition);
            this.Top = Control.MousePosition.Y - 10;
            this.Left = Control.MousePosition.X - (width / 2);
            this.Height = height;
            this.Width = width;
            WinState = true;
        }


        void Maximize()
        {
            // doubleClick = true;
            WinState = false;
            width = okno.ActualWidth;
            height = okno.ActualHeight;
            Screen _screen = Screen.FromPoint(Control.MousePosition);
            this.Top = _screen.WorkingArea.Top;
            this.Left = _screen.WorkingArea.Left;
            this.Height = _screen.WorkingArea.Height;
            this.Width = _screen.WorkingArea.Width;



        }

        private void mainwin_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
