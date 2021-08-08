using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Gantt.Model;
using Gantt.ViewModels.Helpers;

namespace Gantt.ViewModels
{
    public class PlanOfSelectedProjectViewModel : BindableBase, INavigationAware
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        public DelegateCommand CloseApplicationCommand { get; set; }
        public DelegateCommand<PossiblePlanSections?> ChangePlanViewCommand { get; set; }
        public PlanOfSelectedProjectViewModel(IRegionManager regionManager)
        {
            CloseApplicationCommand = new DelegateCommand(CloseApplication);
            ChangePlanViewCommand = new DelegateCommand<PossiblePlanSections?>(ChangePlanView);
            _regionManager = regionManager;
        }
        
        void ChangePlanView(PossiblePlanSections? RequestedPlanSectionType)
        {

            PlanSectionsFactory SectionsFactory = new PlanSectionsFactory(_regionManager);
            ProjectPlanSection RequestedSection = SectionsFactory.MakeSection(RequestedPlanSectionType ?? PossiblePlanSections.PurposeAndScope);
            RequestedSection.ActivateSection();
        }
        void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
            //throw new NotImplementedException();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
