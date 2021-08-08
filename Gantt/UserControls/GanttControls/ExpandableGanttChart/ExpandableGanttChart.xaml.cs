using Gantt.Model;
using ProjectModels;
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

namespace Gantt.UserControls.GanttControls.ExpandableGanttChart
{
    /// <summary>
    /// Logika interakcji dla klasy ExpandableGanttChart.xaml
    /// </summary>
    public partial class ExpandableGanttChart : UserControl
    {
        public ExpandableGanttChart()
        {
            InitializeComponent();
        }

        bool Resize;
        public void cos()
        {
            int h = 0;

        }

        public Action SelectionChange
        {
            get { return (Action)GetValue(SelectionChangeProperty); }
            set { SetValue(SelectionChangeProperty, value); }
        }

        public static readonly DependencyProperty SelectionChangeProperty = DependencyProperty.Register("SelectionChange", typeof(Action), typeof(ExpandableGanttChart));


        public Action<double> MoveTaskAction
        {
            get { return (Action<double>)GetValue(MoveTaskActionProperty); }
            set { SetValue(MoveTaskActionProperty, value); }
        }

        public static readonly DependencyProperty MoveTaskActionProperty = DependencyProperty.Register("MoveTaskAction", typeof(Action<double>), typeof(ExpandableGanttChart));

        public Action<double> ResizeAction
        {
            get { return (Action<double>)GetValue(ResizeActionProperty); }
            set { SetValue(ResizeActionProperty, value); }
        }

        public static readonly DependencyProperty ResizeActionProperty = DependencyProperty.Register("ResizeAction", typeof(Action<double>), typeof(ExpandableGanttChart));


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ExpandableGanttChart));


        public ObservableCollection<ProjectTask> ListaT
        {
            get { return (ObservableCollection<ProjectTask>)GetValue(ListaProperty); }
            set { SetValue(ListaProperty, value); }
        }

        public static readonly DependencyProperty ListaProperty = DependencyProperty.Register("ListaT", typeof(ObservableCollection<ProjectTask>), typeof(ExpandableGanttChart));

        public ProjectTask SelectedTask
        {
            get { return (ProjectTask)GetValue(SelectedTaskProperty); }
            set { SetValue(SelectedTaskProperty, value); }
        }

        public static readonly DependencyProperty SelectedTaskProperty = DependencyProperty.Register("SelectedTask", typeof(ProjectTask), typeof(ExpandableGanttChart));




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
                if (SelectedTask != null)
                {
                    if (!SelectedTask.hasChildren)
                    {//   if (!SelectedTask.isCategory)
                     //  {
                        pos2 = e.GetPosition(win);
                        int h = 0;
                        double g = (pos2.X - pos.X) / 40;
                        if (Math.Abs(g) > 1)
                        {
                            if (Resize)
                                ResizeAction?.Invoke(g);
                            else
                                MoveTaskAction?.Invoke(g);
                            pos.X = pos2.X;
                        }
                    }
                    //   }
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
                if (!SelectedTask.Equals((sender as Label).Tag as ProjectTask))
                    SelectedTask.IsSelected = false;
            }
            SelectedTask = (sender as Label).Tag as ProjectTask;
            //    if(!SelectedTask.isCategory)
            if (!SelectedTask.hasChildren)
            {
                SelectedTask.IsSelected = true;
                SelectionChange?.Invoke();
            }
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
            if (SelectedTask != null)
            {
                //   if (!SelectedTask.isCategory)
                //  {
                if (taskPressed)
                {
                    if (!SelectedTask.hasChildren)
                    {
                        pos2 = e.GetPosition(win);
                        int h = 0;
                        double g = (pos2.X - pos.X) / 40;
                        if (Math.Abs(g) > 1)
                        {
                            if (Resize)
                                ResizeAction?.Invoke(g);
                            else
                                MoveTaskAction?.Invoke(g);
                            pos.X = pos2.X;
                        }
                    }

                }
                // }
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
                //    if (!SelectedTask.isCategory)
                //   {
                if (SelectedTask != null)
                {
                    if (!SelectedTask.Equals((sender as Label).Tag as ProjectTask))
                        SelectedTask.IsSelected = false;
                }
                SelectedTask = (sender as Label).Tag as ProjectTask;
                SelectionChange?.Invoke();
                SelectedTask.IsSelected = true;
                taskPressed = true;
                Resize = true;
                //  }
            }
        }

        private void mn_KeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            if (key == Key.Delete)
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

