using MyGantt.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MyGantt.ViewModels
{
    public class GanttViewModel : BindableBase
    {

    

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand),
              typeof(GanttViewModel), new PropertyMetadata(null));

        private Action<double> _resizeAction;
        public Action<double> ResizeAction
        {
            get { return _resizeAction; }
            set { SetProperty(ref _resizeAction, value); }
        }

        public DelegateCommand<int?> ActivateCommand { get; private set; }
        public DelegateCommand<int?> MyCmd { get; private set; }

        public DelegateCommand SelectedCommand { get; private set; }
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private GantTask _selectedTask;
        public GantTask SelectedTask
        {
            get { return _selectedTask; }
            set { SetProperty(ref _selectedTask, value); }
        }

        private ObservableCollection<GantTask> _lista;
        public ObservableCollection<GantTask> Lista
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

        public GanttViewModel()
        {
            Lista = new ObservableCollection<GantTask>();
            Lista.Add(new GantTask { Name = "Projekt elektryczny", Start = new DateTime(2020, 12, 23), Duration = 5 });
            Lista.Add(new GantTask { Name = "Dobór czujników", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Lista.Add(new GantTask { Name = "Zamówienia", Start = new DateTime(2021, 1, 1), Duration = 5 });
            Lista.Add(new GantTask { Name = "Prefabrykacja szafy", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Lista.Add(new GantTask { Name = "Dwa", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Lista.Add(new GantTask { Name = "Dwa", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Lista.Add(new GantTask { Name = "Dwa", Start = new DateTime(2021, 1, 4), Duration = 4, IsCompleted = true });
            Lista.Add(new GantTask { Name = "Dwa", Start = new DateTime(2021, 1, 4), Duration = 4 });
            Lista[1].AddPredecessor(Lista[0]);
            Lista[1].AddPredecessor(Lista[2]);
            Lista[3].AddPredecessor(Lista[0]);
            Lista[4].AddPredecessor(Lista[1]);
            foreach (GantTask gt in Lista)
            {
                gt.UpdateState();
            }
            Lista.CollectionChanged += Lista_CollectionChanged;
            ResizeAction += Resize;
            MyCmd = new DelegateCommand<int?>(Cmd);
            ActivateCommand = new DelegateCommand<int?>(Activate);
            SelectedCommand = new DelegateCommand(OnListSelection);
            Os = new ObservableCollection<cOs>();
            StartT = new DateTime(2020, 12, 17);
            StopT = new DateTime(2021, 2, 1);
            DateTime TempT = StartT.AddDays(0);
            while (TempT.CompareTo(StopT) <= 0)
            {

                Os.Add(new cOs { Dat = TempT });
                TempT = TempT.AddDays(1);
            }
     

            int h = 0;
        }

        GantTask PrevTask;
        void OnListSelection()
        {
            if (PrevTask != null)
            {
                PrevTask.IsSelected = false;
            }
            if (SelectedTask != null)
            {
                SelectedTask.IsSelected = true;
                PrevTask = SelectedTask;
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
            SelectedTask.MoveStartDate(h);
            RefreshAxis();
        }

        void RefreshAxis()
        {

            GantTask firstTask = Lista.OrderBy((t) => { return t.Start; }).FirstOrDefault();
            StartT = firstTask.Start.AddDays(-1);
            GantTask lastTask = Lista.OrderByDescending((t) => { return t.End; }).FirstOrDefault();
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
            var newStart = Lista.Min((t) => { return t.Start; });

            if (!StartT.Equals(newStart))
            {
                StartT = newStart;
                RefreshAxis();
            }
            var newStop = Lista.Max((t) => { return t.End; });
            if (!StopT.Equals(newStop))
            {
                StopT = newStop;
                RefreshAxis();
            }
            int h = 0;
            // throw new NotImplementedException();
        }

        public void Cmd(int? h)
        {
            //     SelectedTask.BrakeRelations();
            //  Lista.Remove(SelectedTask);
            // //  Lista[2].Predecessor = Lista[0];

            //  Lista[0] = temp;
        }
        public void Activate(int? h)
        {
            //     SelectedTask.BrakeRelations();
            //  Lista.Remove(SelectedTask);
            // //  Lista[2].Predecessor = Lista[0];
        
            //  Lista[0] = temp;
         
        }

    }
}
