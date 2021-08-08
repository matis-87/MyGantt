using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.Model
{
    public class GantTask : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int Id;

        public string Name { get; set; }
        private int _duration;

        public ObservableCollection<GantTask> CategoryContainer { get; set; }
        public GantTask Category;
        public bool isCategory { get; set; }

        public int Duration
        {
            get { return _duration; }
            set
            {
                if (value != _duration)
                {
                    _duration = value;


                    // End = Start.AddDays(_duration);
                    NotifyPropertyChanged();
                    if (Successors.Count > 0)
                        foreach (GantTask t in Successors)
                            t.Refresh();
                    UpdateState();

                }
            }
        }

        private int _nwduration;



        public int NWDuration
        {
            get { return _nwduration; }
            set
            {
                if (value != _nwduration)
                {
                    _nwduration = value;
                    NotifyPropertyChanged();
                }
            }
        }


        int _state;

        public int State
        {
            get { return _state; }
            set
            {
                if (value != _state)
                {
                    _state = value;
                    NotifyPropertyChanged();
                }
            }
        }

        bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                if (value != _isCompleted)
                {
                    _isCompleted = value;
                    NotifyPropertyChanged();
                }
            }
        }

        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private List<int> _lag;

        public List<int> Lag
        {
            get { return _lag; }
            set
            {
                if (value != _lag)
                {
                    _lag = value;
                    NotifyPropertyChanged();
                }
            }
        }

        ObservableCollection<GantTask> _predecessors;
        public ObservableCollection<GantTask> Predecessors
        {
            get { return _predecessors; }
            set
            {
                if (value != _predecessors)
                {
                    _predecessors = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<GantTask> Successors { get; set; }

        public GantTask()
        {
            Predecessors = new ObservableCollection<GantTask>();
            Successors = new ObservableCollection<GantTask>();
            CategoryContainer = new ObservableCollection<GantTask>();
            Category = new GantTask();
            Lag = new List<int>();
          
            Predecessors.CollectionChanged += Predecessor_CollectionChanged;
            Successors.CollectionChanged += Successors_CollectionChanged;
           CategoryContainer.CollectionChanged += CategoryCollectionChanged;
        }

        private void CategoryCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshCategory();
        }

      public  void RefreshCategory()
        {
            if (CategoryContainer.Count > 0)
            {
                var min = CategoryContainer.Min((t) => { return t.Start; });
                var max = CategoryContainer.Max((t) => { return t.End; });
                this.Start = min;
                this.End = max;
                var diff = max.Subtract(min);
                Duration = Convert.ToInt32(diff.TotalDays)+1;
                int h = 0;
            }
        }

        private void Successors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (GantTask s in Successors)
            {
                s.Refresh();
            }
        }

        private void Predecessor_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < _predecessors.Count; i++)
            {
                _predecessors[i].Successors.Add(this);
            }
        }

        public void Refresh()
        {

            if (Predecessors.Count == 0)
                Start = _start;
            else
            {
                List<DateTime> tempEnds = new List<DateTime>();
                for (int i = 0; i < Predecessors.Count; i++)
                {
                    Predecessors[i].UpdateState();
                    tempEnds.Add(Predecessors[i].End.AddDays(Lag[i]));
                }
                DateTime longest = tempEnds.OrderByDescending((t) => { return t.Date; }).FirstOrDefault();

                Start = longest;

            }
      
            //  UpdateState();
        }
        int nwd;
        public void UpdateState()
        {
            DateTime Today = DateTime.Now;


            End = Start.AddDays(Duration - 1);
            NWDuration = Duration - NonWorkingDays(Start, End);
            int resEnd = Today.CompareTo(End.AddDays(1));
            int resStart = Today.CompareTo(Start);
            if ((resEnd > 0) && (resStart > 0))
                State = 0;
            if ((resStart < 0) && (resEnd < 0))
                State = 1;
            if ((resStart >= 0) && (resEnd <= 0))
                State = 2;
            if (IsCompleted)
                State = 3;
            foreach (GantTask gt in Successors)
            {
                gt.UpdateState();
            }
        }
        DateTime _start;
        public DateTime Start
        {
            get
            {
                if (Predecessors == null)
                    return _start;
                else
                    return _start; // Predecessor.End.AddDays(Lag);
            }
            set
            {
                _start = value;
                NotifyPropertyChanged();

                if (Successors.Count > 0)
                    foreach (GantTask t in Successors)
                        t.Refresh();
            //    Category.RefreshCategory();
            }
        }
        private DateTime _end;
        public DateTime End
        {
            get
            {
                return _end;
            }
            set
            {
                if (value != _end)
                {
                    _end = value;
                    NotifyPropertyChanged();
                }
            }

        }

        int lastnwd = 0;
        public void MoveStartDate(double days)
        {
            Start = Start.AddDays(Convert.ToInt32(days));
            for (int i = 0; i < Lag.Count; i++)
            {
                int tmp = Convert.ToInt32(days);
                Lag[i] += tmp;

            }

            UpdateState();
        
        }

        public void Resize(double days)
        {

            if (Duration + Convert.ToInt32(days) > 0)
                Duration += Convert.ToInt32(days);

            UpdateState();

        }
        int ij = 1;
        public void AddPredecessor(GantTask Predecessor)
        {
            Lag.Add(ij);
            Predecessors.Add(Predecessor);


            ij += 4;
        }

        public void BrakeRelations()
        {
            for (int i = 0; i < Successors.Count; i++)
            {
                Successors[i].Predecessors.Remove(this);
            }
        }

        public int NonWorkingDays(DateTime start, DateTime stop)
        {
            int nonwrokingDays = 0;
            double diff = stop.Subtract(start).TotalDays;
            int weeks = Convert.ToInt32(Math.Truncate(diff / 7));
            int weeksDays = weeks * 7;
            int restDays = Convert.ToInt32(diff) - weeksDays;
            int dayofWeek = (int)start.DayOfWeek;
            if (dayofWeek + restDays == 6)
                nonwrokingDays = 1;
            if (dayofWeek + restDays >= 7)
                nonwrokingDays = 2;
            nonwrokingDays += weeks * 2;
            if (start.DayOfWeek == DayOfWeek.Sunday)
                nonwrokingDays++;
            return nonwrokingDays;

        }

        public override bool Equals(object obj)
        {

            GantTask temp = obj as GantTask;
            if (temp == null)
                return false;
            if ((Name.Equals(temp.Name)) && (Duration == temp.Duration) && (Start.Equals(temp.Start)) && (End.Equals(temp.End)) && (State == temp.State))
                return true;
            else
                return false;
        }
    }
}
