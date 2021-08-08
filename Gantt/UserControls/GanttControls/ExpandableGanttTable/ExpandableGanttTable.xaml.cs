using Gantt.Model;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Gantt.UserControls.GanttControls.ExpandableGanttTable
{
    /// <summary>
    /// Logika interakcji dla klasy ExpandableGanttTable.xaml
    /// </summary>
    public partial class ExpandableGanttTable : UserControl, INotifyPropertyChanged
    {
        public ExpandableGanttTable()
        {
            InitializeComponent();
        }
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
        bool _clipboardHasValue;
        public bool ClipboardHasValue
        {
            get { return _clipboardHasValue; }
            set
            {
                if (value != _clipboardHasValue)
                {
                    _clipboardHasValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        ProjectTask TaskClipboard;

        public static readonly DependencyProperty ItemsSourceProperty =
    DependencyProperty.Register("ItemsSource", typeof(object),
      typeof(ExpandableGanttTable), new PropertyMetadata(null));

        public ProjectTask SelectedTask
        {
            get
            {

                return (ProjectTask)GetValue(SelectedTaskProperty);
            }
            set
            {

                SetValue(SelectedTaskProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedTaskProperty =
    DependencyProperty.Register("SelectedTask", typeof(ProjectTask),
      typeof(ExpandableGanttTable), new PropertyMetadata(null));


        ProjectTask LastSelectedTask;

        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
    
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedTask != null)
            {
                if (!SelectedTask.Equals(e.NewValue as ProjectTask))
                    SelectedTask.IsSelected = false;
                if (LastSelectedTask != null)
                {
                    if (!SelectedTask.Equals(LastSelectedTask))
                    {
                        LastSelectedTask.IsSelected = false;
                    }
                   
                }
                LastSelectedTask = SelectedTask;
                LastSelectedTask.IsSelected = false;
            }
                SelectedTask = e.NewValue as ProjectTask;
                SelectedTask.IsSelected = true;
          
        }

        private void MenuItem_InsertTask(object sender, RoutedEventArgs e)
        {
            ProjectTask ParentTask = (sender as MenuItem).Tag as ProjectTask;
            ProjectTask Newtask = new ProjectTask();
            Newtask.Name = "Nowe zadanie";
            ParentTask.AddChild(Newtask);

        }

        private void MenuItem_Cut(object sender, RoutedEventArgs e)
        {
            ProjectTask CurrentTask = (sender as MenuItem).Tag as ProjectTask;
            TaskClipboard = CurrentTask.Clone() as ProjectTask;
            CurrentTask.RemoveTask();

            }

        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
            ProjectTask CurrentTask = (sender as MenuItem).Tag as ProjectTask;
            CurrentTask.RemoveTask();
        }

        private void MenuItem_PasteTask(object sender, RoutedEventArgs e)
        {
            if (TaskClipboard != null)
            {
                ProjectTask LocalTaskClipboard = new ProjectTask();
                LocalTaskClipboard = TaskClipboard.Clone();
                ProjectTask CurrentTask = (sender as MenuItem).Tag as ProjectTask;
                CurrentTask.AddChild(LocalTaskClipboard);
                ClipboardHasValue = false;
            }
        }

        private void MenuItem_CopyTask(object sender, RoutedEventArgs e)
        {
            ProjectTask CurrentTask = (sender as MenuItem).Tag as ProjectTask;
            TaskClipboard = CurrentTask.Clone() as ProjectTask;
            ClipboardHasValue = true;
        }

        private void MenuItem_ChangeExecutionStatus(object sender, RoutedEventArgs e)
        {

            ProjectTask CurrentTask = (sender as MenuItem).Tag as ProjectTask;
            CurrentTask.IsCompleted = !CurrentTask.IsCompleted;

        }
    }
}
