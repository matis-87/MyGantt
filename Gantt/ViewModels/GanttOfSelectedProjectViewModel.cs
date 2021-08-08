using EventAgregator;
using Gantt.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Gantt.ViewModels
{
    public class GanttOfSelectedProjectViewModel : BindableBase, INavigationAware
    {
        IDialogService _dialogService;
        IEventAggregator _ea;
        public DelegateCommand OpenTaskSettingsPopUpCommand { get; set; }
        public DelegateCommand TestCommand { get; set; }

        private Action<double> _resizeAction;
        public Action<double> ResizeAction
        {
            get { return _resizeAction; }
            set { SetProperty(ref _resizeAction, value); }
        }

        private Action<double> _moveTaskAction;
        public Action<double> MoveTaskAction
        {
            get { return _moveTaskAction; }
            set { SetProperty(ref _moveTaskAction, value); }
        }


        private Action _selectionChange;
        public Action SelectionChange
        {
            get { return _selectionChange; }
            set { SetProperty(ref _selectionChange, value); }
        }


        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ProjectTask _selectedTask;
        public ProjectTask SelectedTask
        {
            get { return _selectedTask; }
            set { SetProperty(ref _selectedTask, value); }
        }

        private ObservableCollection<ProjectTask> _lista;
        public ObservableCollection<ProjectTask> Tasks
        {
            get { return _lista; }
            set { SetProperty(ref _lista, value); }
        }

        private ObservableCollection<cOs> _os;
        public ObservableCollection<cOs> Os
        {
            get { return _os; }
            set { SetProperty(ref _os, value); }
        }
        DateTime _startT;
        public DateTime StartT
        {
            get { return _startT; }
            set { SetProperty(ref _startT, value); }
        }

        DateTime _stopT;
        public DateTime StopT
        {
            get { return _stopT; }
            set { SetProperty(ref _stopT, value); }
        }



        ProjectTask PrevTask;
        public GanttOfSelectedProjectViewModel(IDialogService dialogService, IEventAggregator ea)
        {
            _dialogService = dialogService;
            _ea = ea;
            OpenTaskSettingsPopUpCommand = new DelegateCommand(OpenTaskSettingsPopUp);
            TestCommand = new DelegateCommand(SendMessage);
            Tasks = new ObservableCollection<ProjectTask>();
            Tasks.Add(new ProjectTask { Name = "1 Ogólne", Start = new DateTime(2020, 12, 23), Duration = 5 });
            Tasks.Add(new ProjectTask { Name = "2 Dobór czujników", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Tasks.Add(new ProjectTask { Name = "3 Zamówienia", Start = new DateTime(2021, 1, 1), Duration = 5 });
            Tasks.Add(new ProjectTask { Name = "4 Prefabrykacja szafy", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Tasks.Add(new ProjectTask { Name = "5 Aranżacja", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Tasks.Add(new ProjectTask { Name = "6 Dwa", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Tasks.Add(new ProjectTask { Name = "7 Dwa", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Tasks.Add(new ProjectTask { Name = "8 Ogólne", Start = new DateTime(2021, 1, 4), Duration = 4 });
            ProjectTask temp = new ProjectTask { Name = "1.1 Projektowanie", Start = new DateTime(2020, 12, 23), Duration = 5 };
            temp.AddChild(new ProjectTask { Name = "1.1.1 Określenie funkcjonalności", Start = new DateTime(2021, 1, 4), Duration = 4 });
            temp.AddChild(new ProjectTask { Name = "1.1.2 Projekt elektryczny", Start = new DateTime(2021, 1, 6), Duration = 5 });
            //Tasks[1].AddPredecessor(Tasks[0].Children[0]);
            Tasks[0].AddChild(temp);
            //Tasks[1].Predecessors.Add(Tasks[0].Children[0]);
            foreach (ProjectTask gt in Tasks)
            {
                gt.UpdateState();
            }
            //  Tasks.CollectionChanged += Lista_CollectionChanged;
            ResizeAction += Resize;
            MoveTaskAction += act;
            Os = new ObservableCollection<cOs>();
            StartT = new DateTime(2020, 12, 17);
            StopT = new DateTime(2021, 2, 1);
            DateTime TempT = StartT.AddDays(0);
            while (TempT.CompareTo(StopT) <= 0)
            {

                Os.Add(new cOs { Dat = TempT });
                TempT = TempT.AddDays(1);
            }

            SelectionChange += OnSelectionChange;
            int h = 0;
      
        }
  
        void OnSelectionChange()
        {
            _ea.GetEvent<MessageSentEvent>().Publish(SelectedTask);
        }

        private void SendMessage()
        {

          
        }
        public void OpenTaskSettingsPopUp()
        {
            DialogParameters dialog = new DialogParameters();
            dialog.Add("task", SelectedTask);
            dialog.Add("tasks", Tasks);
            _dialogService.Show("EditTaskProperties", dialog, r =>
            {

            });


        }

        void OnListSelection()
        {
            if (PrevTask != null)
            {
                PrevTask.IsSelected = false;
            }

            if (SelectedTask != null)
            {
                if (!SelectedTask.hasChildren)
                {
                    SelectedTask.IsSelected = true;
                    PrevTask = SelectedTask;
                }
            }
            int h = 0;
        }
        void Resize(double h)
        {
            SelectedTask.Resize(h);
            RefreshAxis();
        }
        void act(double h)
        {
            double m = h;
            SelectedTask.MoveStartDate(m);
            RefreshAxis();
        }

        void RefreshAxis()
        {

            ProjectTask firstTask = Tasks.OrderBy((t) => { return t.Start; }).FirstOrDefault();
            StartT = firstTask.Start.AddDays(-1);
            ProjectTask lastTask = Tasks.OrderByDescending((t) => { return t.End; }).FirstOrDefault();
            StopT = lastTask.End.AddDays(1);
            TimeSpan diff = StopT.Subtract(StartT);
            if (diff.TotalDays < 30)
            {
                StopT = StartT.AddDays(30);
            }
            DateTime TempT = StartT.AddDays(0);
            Os.Clear();
            while (TempT.CompareTo(StopT) <= 0)
            {

                Os.Add(new cOs { Dat = TempT });
                TempT = TempT.AddDays(1);
            }
        }
        private void Lista_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var newStart = Tasks.Min((t) => { return t.Start; });

            if (!StartT.Equals(newStart))
            {
                StartT = newStart;
                RefreshAxis();
            }
            var newStop = Tasks.Max((t) => { return t.End; });
            if (!StopT.Equals(newStop))
            {
                StopT = newStop;
                RefreshAxis();
            }
            int h = 0;
            // throw new NotImplementedException();
        }

    
        public void Activate(int? h)
        {
            //     SelectedTask.BrakeRelations();
            //  Lista.Remove(SelectedTask);
            // //  Lista[2].Predecessor = Lista[0];

            //  Lista[0] = temp;

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
            Tasks = navigationContext.Parameters["tree"] as ObservableCollection<ProjectTask>;

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;   //throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           // throw new NotImplementedException();

           
        }
    }
}
