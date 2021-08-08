using Gantt.Model.PurposeAndScope;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gantt.ViewModels
{
    public class PurposeAndScopeViewModel : BindableBase
    {
        private ObservableCollection<ProjectCompromiseItem> _projectCompromises;
      public ObservableCollection<ProjectCompromiseItem> ProjectCompromises
        {
            get { return _projectCompromises; }
            set { SetProperty(ref _projectCompromises, value); }
        }
        public PurposeAndScopeViewModel()
        {
            ProjectCompromises = new ObservableCollection<ProjectCompromiseItem>
           {
               new ProjectCompromiseItem{ Name = "Czas"},
               new ProjectCompromiseItem{ Name = "Koszt"}
            };

            ProjectCompromises.Add(new ProjectCompromiseItem { Name = "ggg" });
           
        }
    }
}
