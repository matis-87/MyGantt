using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ProjectsMenagament.ViewModels
{
    public class MainWindowViewModel : BindableBase, INavigationAware
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand ViewACommand { get; set; }
        public DelegateCommand TotalProjectsCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {

            ViewACommand = new DelegateCommand(GoToViewA);
            TotalProjectsCommand = new DelegateCommand(GoToTotalProjects);
            _regionManager = regionManager;
        }

        void GoToTotalProjects()
        {
            _regionManager.RequestNavigate("ContentRegion", "TotalProjectsSchedule");
        }
        void GoToViewA()
        {
            _regionManager.RequestNavigate("ContentRegion", "PlanOfSelectedProject");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            int j = 0;
          //  throw new System.NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new System.NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
          //  throw new System.NotImplementedException();
        }
    }
}
