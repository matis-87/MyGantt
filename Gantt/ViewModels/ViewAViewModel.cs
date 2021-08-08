using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.ViewModels
{
    public class ViewAViewModel : BindableBase, INavigationAware
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        public DelegateCommand<int?> ActivateCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; set; }
        public ViewAViewModel(IRegionManager regionManager)
        {
            Message = "View A from your Prism Module";
            ActivateCommand = new DelegateCommand<int?>(Activate);
            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
            _regionManager = regionManager;
        }
        public void Activate(int? h)
        {
            //     SelectedTask.BrakeRelations();
            //  Lista.Remove(SelectedTask);
            // //  Lista[2].Predecessor = Lista[0];

            //  Lista[0] = temp;
            int j = 0;
            _regionManager.RequestNavigate("ContentRegion2", "Gantt");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            GoForwardCommand.RaiseCanExecuteChanged();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //  throw new NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            int h = 0;
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
