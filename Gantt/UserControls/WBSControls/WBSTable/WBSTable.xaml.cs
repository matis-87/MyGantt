using Gantt.Model;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Gantt.UserControls.WBSControls.WBSTable
{
    /// <summary>
    /// Logika interakcji dla klasy WBSTable.xaml
    /// </summary>
    public partial class WBSTable : UserControl, INotifyPropertyChanged 
    {
        public WBSTable()
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

        public static readonly DependencyProperty ItemsSourceProperty =
    DependencyProperty.Register("ItemsSource", typeof(object),
      typeof(WBSTable), new PropertyMetadata(null));

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
       typeof(WBSTable), new PropertyMetadata(null));

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
        public event PropertyChangedEventHandler PropertyChanged;
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ProjectTask TaskClipboard;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProjectTask task = (sender as Button).Tag as ProjectTask;
            int h = 0;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectTask newTask = new ProjectTask();
            newTask.Name = "Nowe zadanie";
            ProjectTask task = (sender as MenuItem).Tag as ProjectTask;
            task.AddChild(newTask);
        }

        private void Popup_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_InsertTask(object sender, RoutedEventArgs e)
        {
            ProjectTask ParentTask = (sender as MenuItem).Tag as ProjectTask;
            ProjectTask Newtask = new ProjectTask();
            Newtask.Name = "Nowe zadanie";
            ParentTask.AddChild(Newtask);

        }

        private void MenuItem_RemoveTask(object sender, RoutedEventArgs e)
        {
           if(MessageBox.Show("Czy napewno chcesz usunąć to zadanie?", "Usuń zadanie",MessageBoxButton.YesNo)== MessageBoxResult.Yes)
            {
                ProjectTask task = (sender as MenuItem).Tag as ProjectTask;
                if (isChildTask(task))
                {
                    RemoveChildTask(task);
                }
                else
                {
                    RemoveRootTask(task);
                }
            }
        }
        bool isChildTask(ProjectTask task)
        {
            if (task.Parent != null)
                return true;
            return false;
        }
        void RemoveChildTask(ProjectTask task)
        {
            task.RemoveTask();
        }
        void RemoveRootTask(ProjectTask task)
        {
            ObservableCollection<ProjectTask> Tasks = ItemsSource as ObservableCollection<ProjectTask>;
            int index = Tasks.Select((x, i) => object.Equals(x, task) ? i : -1).Where(x => x != -1).FirstOrDefault();
            Tasks.RemoveAt(index);
            task.Parent.UpdateWbsCode();
        }

        private void MenuItem_CopyTask(object sender, RoutedEventArgs e)
        {
            ProjectTask CurrentTask = (sender as MenuItem).Tag as ProjectTask;
            TaskClipboard = CurrentTask.Clone() as ProjectTask;
            ClipboardHasValue = true;
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

        private void MenuItem_Cut(object sender, RoutedEventArgs e)
        {
            ProjectTask CurrentTask = (sender as MenuItem).Tag as ProjectTask;
            TaskClipboard = CurrentTask.Clone() as ProjectTask;
            if (isChildTask(CurrentTask))
            {
                RemoveChildTask(CurrentTask);
            }
            else
            {
                RemoveRootTask(CurrentTask);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
