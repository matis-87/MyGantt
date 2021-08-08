using Gantt.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ProjectModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gantt.ViewModels
{
    public class RACIViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<ProjectTask> _tasks;
        public ObservableCollection<ProjectTask> Tasks
        {
            get { return _tasks; }
            set { SetProperty(ref _tasks, value); }
        }

        public ObservableCollection<TeamMember> TeamMembers;

        public RACIViewModel()
        {
            TeamMembers = new ObservableCollection<TeamMember>();
            TeamMembers.Add(new TeamMember { Name = "Mateusz Skuza" });
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Tasks = navigationContext.Parameters["tree"] as ObservableCollection<ProjectTask> ?? null;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}
