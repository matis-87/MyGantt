using Gantt.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace Gantt.ViewModels
{
    public class WBSViewModel : BindableBase, INavigationAware
    {
        public DelegateCommand AddNewTaskCommand { get; set; }
        ListCollectionView _collectionView;
        public ListCollectionView CollectionView
        {
            get { return _collectionView; }
            set {
                SetProperty(ref _collectionView, value);
            }
        }
        public ProjectTask RootTask;
        public  ObservableCollection<ProjectTask> Tasks { get; set; }
        public List<ProjectTask> SortedTasks { get; set; }
        public WBSViewModel()
        {

            //  Tasks[0].AddPredecessor(Tasks[1]);
            AddNewTaskCommand = new DelegateCommand(AddNewTask);
            Tasks = new ObservableCollection<ProjectTask>();
            RootTask = new ProjectTask();
            RootTask.Name = "Root";
            RootTask.Children = Tasks;
            RootTask.AddChild(new ProjectTask { Name = "1 Ogólne", Start = new DateTime(2020, 12, 23), Duration = 5 });
            RootTask.AddChild(new ProjectTask { Name = "2 Dobór czujników", Start = new DateTime(2021, 1, 4), Duration = 4 });
            RootTask.AddChild(new ProjectTask { Name = "3 Zamówienia", Start = new DateTime(2021, 1, 1), Duration = 5 });
            RootTask.AddChild(new ProjectTask { Name = "4 Prefabrykacja szafy", Start = new DateTime(2021, 1, 4), Duration = 4 });
            RootTask.AddChild(new ProjectTask { Name = "5 Aranżacja", Start = new DateTime(2021, 1, 4), Duration = 4 });
            RootTask.AddChild(new ProjectTask { Name = "6 Dwa", Start = new DateTime(2021, 1, 4), Duration = 4 });
            RootTask.AddChild(new ProjectTask { Name = "7 Dwa", Start = new DateTime(2021, 1, 4), Duration = 4 });
            RootTask.AddChild(new ProjectTask { Name = "8 Ogólne", Start = new DateTime(2021, 1, 4), Duration = 4 });
            ProjectTask temp = new ProjectTask { Name = "1.1 Projektowanie", Start = new DateTime(2020, 12, 23), Duration = 5 };
            Tasks[0].AddChild(temp);
            temp.AddChild(new ProjectTask { Name = "1.1.1 Określenie funkcjonalności", Start = new DateTime(2021, 1, 4), Duration = 4 });
            temp.AddChild(new ProjectTask { Name = "1.1.2 Projekt elektryczny", Start = new DateTime(2021, 1, 6), Duration = 5 });
            foreach (ProjectTask gt in Tasks)
            {
                gt.UpdateState();
            }
            
            int h = 0;
        }
        public void AddNewTask()
        {
            ProjectTask temp = new ProjectTask { Name = "Nowe zadanie"};
            RootTask.AddChild(temp);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // throw new NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add("tree", Tasks);
        }
    }
}
