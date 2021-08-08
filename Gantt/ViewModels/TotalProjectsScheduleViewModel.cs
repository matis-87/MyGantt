using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gantt.ViewModels
{
    public class TotalProjectsScheduleViewModel : BindableBase, INavigationAware
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;

        public DelegateCommand<int?> ActivateCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; set; }

        public TotalProjectsScheduleViewModel(IRegionManager regionManager)
        {
           // ActivateCommand = new DelegateCommand<int?>(Activate);
            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
            _regionManager = regionManager;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // throw new NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           // throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            GoForwardCommand.RaiseCanExecuteChanged();
        }
        private void GoForward()
        {
            _journal.GoForward();
        }
        private bool CanGoForward()
        {
            return _journal != null && _journal.CanGoForward;
        }
    }
}
