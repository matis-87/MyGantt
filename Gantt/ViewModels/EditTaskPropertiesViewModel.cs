using EventAgregator;
using Gantt.Dialogs;
using Gantt.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gantt.ViewModels
{
    public class EditTaskPropertiesViewModel : DialogViewModelBase
    {
        #region Properties
        private string _taskName = "Prism Application";
        public string TaskName
        {
            get { return _taskName; }
            set { SetProperty(ref _taskName, value); }
        }

        private string _selectionName;
        public string SelectionName
        {
            get { return _selectionName; }
            set { SetProperty(ref _selectionName, value); }
        }

        private bool _isPredecessorChoisingModeActive;
        public bool IsPredecessorChoisingModeActive
        {
            get { return _isPredecessorChoisingModeActive; }
            set { SetProperty(ref _isPredecessorChoisingModeActive, value); }
        }

        private ProjectTask _currentTask;
        public ProjectTask CurrentTask
        {
            get { return _currentTask; }
            set { SetProperty(ref _currentTask, value); }
        }

        private ObservableCollection<Precedessor> _currentTaskPredecessors;
        public ObservableCollection<Precedessor> CurrentTaskPredecessors
        {
            get { return _currentTaskPredecessors; }
            set { SetProperty(ref _currentTaskPredecessors, value); }
        }
        #endregion

        #region Fields
        IDialogParameters _parameters;
        IEventAggregator _ea;
        ProjectTask EditedTask;
        ProjectTask Predecessor;
        List<ProjectTask> ListOfPredecessors;
        public DelegateCommand SelectPredecesorCommand { get; set; }
        public DelegateCommand ConfirmCommand { get; set; }
        public DelegateCommand OffsetChangedCommand { get; private set; }
        #endregion
        public EditTaskPropertiesViewModel(IEventAggregator ea)
        {
            _ea = ea;
            Title = "Moj dialogBox";
            ConfirmCommand = new DelegateCommand(Confirm);
            SelectPredecesorCommand = new DelegateCommand(SelectPredecessor);
            OffsetChangedCommand = new DelegateCommand(OnOffsetChanged);
            ea.GetEvent<MessageSentEvent>().Subscribe(GetChosenPredecessor);
        }     
        void SelectPredecessor()
        {
            IsPredecessorChoisingModeActive = true;
        }
        void Confirm()
        {
            CurrentTask.AddPredecessor(new Precedessor { Task = Predecessor });
            IsPredecessorChoisingModeActive = false;
        }
        public void GetChosenPredecessor(ProjectTask _predecessor)
        {
            Predecessor = _predecessor;
            SelectionName = _predecessor.Name;
        }
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            EditedTask = new ProjectTask();
            _parameters = parameters;
            CurrentTask = parameters.GetValue<ProjectTask>("task");
            CurrentTaskPredecessors = CurrentTask.Predecessors;                      
        }

        public void OnOffsetChanged()
        {
            CurrentTask.Refresh();
        }

    }
}
