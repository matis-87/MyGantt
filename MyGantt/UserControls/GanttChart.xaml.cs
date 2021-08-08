using MyGantt.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyGantt.UserControls
{
    /// <summary>
    /// Interaction logic for GanttChart
    /// </summary>
    public partial class GanttChart : UserControl
    {
        public GanttChart()
        {
            InitializeComponent();
            
        }
        bool Resize;
        public void cos()
        {
            int h = 0;

        }
        public Action<double> JumpCommand
        {
            get { return (Action<double>)GetValue(JumpCommandProperty); }
            set { SetValue(JumpCommandProperty, value); }
        }

        public static readonly DependencyProperty JumpCommandProperty = DependencyProperty.Register("JumpCommand", typeof(Action<double>), typeof(GanttChart));

        public Action<double> ResizeAction
        {
            get { return (Action<double>)GetValue(ResizeActionProperty); }
            set { SetValue(ResizeActionProperty, value); }
        }

        public static readonly DependencyProperty ResizeActionProperty = DependencyProperty.Register("ResizeAction", typeof(Action<double>), typeof(GanttChart));


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }     

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(GanttChart));


        public ObservableCollection<GantTask> ListaT
        {
            get { return (ObservableCollection<GantTask>)GetValue(ListaProperty); }
            set { SetValue(ListaProperty, value); }
        }

        public static readonly DependencyProperty ListaProperty = DependencyProperty.Register("ListaT", typeof(ObservableCollection<GantTask>), typeof(GanttChart));

        public GantTask SelectedTask
        {
            get { return (GantTask)GetValue(SelectedTaskProperty); }
            set { SetValue(SelectedTaskProperty, value); }
        }

        public static readonly DependencyProperty SelectedTaskProperty = DependencyProperty.Register("SelectedTask", typeof(GantTask), typeof(GanttChart));




        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Command?.Execute(null);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var but = sender as Button;
            int h = Convert.ToInt32(but.Tag);
          
         //   JumpCommand?.Invoke(h);
        }
        bool taskPressed;
        GantTask PressedTask;
        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos2;
            Window win = Window.GetWindow(this);
        
        
            if (taskPressed)
            {
         
                pos2 = e.GetPosition(win);
                int h = 0;
                double g = (pos2.X - pos.X)/40;
                if ( Math.Abs(g) >1)
                {
                    if(Resize)
                        ResizeAction?.Invoke(g);
                    else    
                        JumpCommand?.Invoke(g);
                    pos.X = pos2.X;
                }
            }
            

        }

        Point pos;
        private void Label_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window win = Window.GetWindow(this);
            pos = e.GetPosition(win);
            if (SelectedTask != null)
            {
                if (!SelectedTask.Equals((sender as Label).Tag as GantTask))
                    SelectedTask.IsSelected = false;
            }
            SelectedTask = (sender as Label).Tag as GantTask;
            SelectedTask.IsSelected = true;
            taskPressed = true;
            
          //  PressedTask = lb.SelectedItem as GantTask;
            int g = 0;
        }

        private void ListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            taskPressed = false;

        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
          //  taskPressed = false;
        }

        private void Button_DragOver(object sender, DragEventArgs e)
        {
            int h = 0;
        }

        private void DockPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void DockPanel_MouseMove(object sender, MouseEventArgs e)
        {

            Point pos2;
            Window win = Window.GetWindow(this);

            if (taskPressed)
            {
                pos2 = e.GetPosition(win);
                int h = 0;
                double g = (pos2.X - pos.X) / 40;
                if (Math.Abs(g) > 1)
                {
                    if (Resize)
                        ResizeAction?.Invoke(g);
                    else
                        JumpCommand?.Invoke(g);
                    pos.X = pos2.X;
                }
            }
        }

        private void DockPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            taskPressed = false;
            Resize = false;
        }

        private void DockPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            taskPressed = false;

            Resize = false;
        }

        private void Label_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Window win = Window.GetWindow(this);
            pos = e.GetPosition(win);

            if (SelectedTask != null)
            {
                if(!SelectedTask.Equals((sender as Label).Tag as GantTask))
                    SelectedTask.IsSelected = false;
            }
            SelectedTask = (sender as Label).Tag as GantTask;
            SelectedTask.IsSelected = true;
            taskPressed = true;
            Resize = true;

        }

        private void mn_KeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            if(key == Key.Delete)
            {
                Command?.Execute(null);
            }
            if (key == Key.Escape)
                SelectedTask.IsSelected = false;
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
        }
    }
}
