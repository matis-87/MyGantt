using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gantt.ViewModels
{
    public class GanttViewModel : BindableBase, INavigationAware
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;

        public GanttViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // throw new NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //  throw new NotImplementedException();
            int h = 0;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
          //  throw new NotImplementedException();
        }
    }
}
