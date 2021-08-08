using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModels
{
  public  class ProjectTask : INotifyPropertyChanged 
    {

        public event PropertyChangedEventHandler PropertyChanged;
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        #region Properties
        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        string _taskDescription;
        public string TaskDescription
        {
            get { return _taskDescription; }
            set
            {
                if (value != _taskDescription)
                {
                    _taskDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }

        string _wbsCode;
        public string WbsCode
        {
            get { return _wbsCode; }
            set
            {
                if (value != _wbsCode)
                {
                    _wbsCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get { return _duration; }
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    NotifyPropertyChanged();
                    if (Successors.Count > 0)
                        foreach (ProjectTask SingleSuccessor in Successors)
                            SingleSuccessor.Refresh();
                    UpdateState();
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

        bool _isExpanded;
        public bool isExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool _isCompleted;
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

        public ProjectTask _parent;
        public ProjectTask Parent
        {

            get { return _parent; }
            set
            {
                if (value != _parent)
                {
                    _parent = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool _hasChildren;
        public bool hasChildren
        {
            get { return _hasChildren; }
            set
            {
                if (value != _hasChildren)
                {
                    _hasChildren = value;
                    NotifyPropertyChanged();
                }
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
                    return _start;
            }
            set
            {
                _start = value;
                NotifyPropertyChanged();
                if (Successors.Count > 0)
                    foreach (ProjectTask t in Successors)
                        t.Refresh();
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
        #endregion

        #region Fields
        public ProjectTask Root;
        public int Id;
        public ObservableCollection<Precedessor> Predecessors { get; set; }
        public ObservableCollection<ProjectTask> Successors { get; set; }
        public ObservableCollection<ProjectTask> Children { get; set; }
        public int NWDuration { get; set; }
        #endregion

        public ProjectTask()
        {
            Predecessors = new ObservableCollection<Precedessor>();
            Successors = new ObservableCollection<ProjectTask>();
            Children = new ObservableCollection<ProjectTask>();
            WbsCode = string.Empty;
            Predecessors.CollectionChanged += Predecessor_CollectionChanged;
            Successors.CollectionChanged += Successors_CollectionChanged;
            Children.CollectionChanged += Children_CollectionChanged;
            Start = new DateTime(DateTime.Now.Year,DateTime.Now.Month, DateTime.Now.Day);
            Duration = 1;           
        }

        public void RefreshParent()
        {
            if (hasChildren)
            {
                var min = Children.Min((t) => { return t.Start; });
                var max = Children.Max((t) => { return t.End; });
                this.Start = min;
                this.End = max;
                var diff = max.Subtract(min);
                Duration = Convert.ToInt32(diff.TotalDays) + 1;
                int h = 0;
            }
            Parent?.RefreshParent();
        }

        private void Children_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Children.Count == 0)
                hasChildren = false;
            else
                hasChildren = true;
        }
        private void Successors_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (ProjectTask s in Successors)
            {
                s.Refresh();
            }
        }

        private void Predecessor_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < Predecessors.Count; i++)
            {
                Predecessors[i].Task.Successors.Add(this);
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
                    Predecessors[i].Task.UpdateState();
                    tempEnds.Add(Predecessors[i].Task.End.AddDays(Predecessors[i].Offset+1));
                }
                DateTime longest = tempEnds.OrderByDescending((t) => { return t.Date; }).FirstOrDefault();
                Start = longest;
            }
        }

        public void RefreshSuccessors()
        {
            foreach (ProjectTask pt in this.Successors)
            {
                pt.Refresh();
            }
        }
        public void MoveStartDate(double days)
        {
            Start = Start.AddDays(Convert.ToInt32(days));
            for (int i = 0; i < Predecessors.Count; i++)
            {
                int tmp = Convert.ToInt32(days);
                Predecessors[i].Offset += tmp;
            }
            UpdateState();
            Parent?.RefreshParent();
        }

        public void Resize(double days)
        {
            if (Duration + Convert.ToInt32(days) > 0)
                Duration += Convert.ToInt32(days);
            UpdateState();
            Parent?.RefreshParent();
        }
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
            foreach (ProjectTask gt in Successors)
            {
                gt.UpdateState();
            }
        }

        public void AddChild(ProjectTask Child)
        {
            Child.Parent = this;
            Children.Add(Child);
            Child.UpdateWbsCode();
            RefreshParent();
        }
        public void AddPredecessor(Precedessor _predecessor)
        {
            Predecessors.Add(_predecessor);
         
        }
        public void AddPredecessors(ObservableCollection<Precedessor> _predecessors)
        {
            foreach(Precedessor pt in _predecessors)
            {
                AddPredecessor(pt);
            }
        }

        public void BrakeRelations()
        {
            for (int i = 0; i < Successors.Count; i++)
            {
                RemovePredecessor(Successors[i].Predecessors, this);
            }
        }

        void RemovePredecessor(ObservableCollection<Precedessor> PredecessorsList, ProjectTask TaskToRemove)
        {
            for(int i = 0; i< PredecessorsList.Count; i ++)
            {
                if(PredecessorsList[i].Task.Equals(TaskToRemove))
                {
                    PredecessorsList.RemoveAt(i);
                }
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
            ProjectTask temp = obj as ProjectTask;
            if (temp == null)
                return false;
            if ((WbsCode.Equals(temp.WbsCode)) && (Name.Equals(temp.Name)) && (Duration == temp.Duration) && (Start.Equals(temp.Start)) && (End.Equals(temp.End)) && (State == temp.State))
                return true;
            else
                return false;
        }
        public ProjectTask Clone()
        {
            ProjectTask CloneTask = new ProjectTask();
            CloneTask.Name = Name;
            CloneTask.TaskDescription = TaskDescription;

            foreach(ProjectTask t in Children)
            {
                CloneTask.AddChild(t.Clone());
            }
            foreach(Precedessor t in Predecessors)
            {
                CloneTask.AddPredecessor(t);
            }

            return CloneTask;
        }

        public void RemoveTask()
        {
            ProjectTask root = this.Parent;
            int index = root.Children.Select((x, i) => object.Equals(x, this) ? i : -1).Where(x => x != -1).FirstOrDefault();
            root.Children.RemoveAt(index);
            RefreshParent();
            this.Parent.UpdateWbsCode();
        }

 
        public void UpdateWbsCode()
        {
            if (Parent == null)
            {
                WbsCode = (GetIndexInChildCollection() + 1).ToString();
            }
            else
            {
                if ((Parent.WbsCode == string.Empty)|| (Parent.WbsCode.Equals("0")))
                {
                    WbsCode = (GetIndexInChildCollection() + 1).ToString();
                }
                else
                    WbsCode = Parent.WbsCode + "." + (GetIndexInChildCollection() + 1).ToString();
            }
           UpdateChildrenWSBCode();
        }
        public int GetIndexInChildCollection()
        {
            if (this.Parent == null)
                return -1;
            ProjectTask Parent = this.Parent;
            int index = Parent.Children.Select((x, i) => object.Equals(x, this) ? i : -1).Where(x => x != -1).FirstOrDefault();
            return index;
            
        }
        public void UpdateChildrenWSBCode()
        {
            foreach(ProjectTask ChildTask in Children)
            {
                ChildTask.UpdateWbsCode();
            }
        }
    }
}
