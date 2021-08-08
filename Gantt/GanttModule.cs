using Gantt.ViewModels;
using Gantt.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Gantt
{
    public class GanttModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RequestNavigate("ContentRegion", "PlanOfSelectedProject");
         //   regionManager.RequestNavigate("SelectedProjectRegion", "GanttOfSelectedProject");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<GanttOfSelectedProject>();
            containerRegistry.RegisterForNavigation<PurposeAndScope>();
            containerRegistry.RegisterForNavigation<TotalProjectsSchedule>();
            containerRegistry.RegisterForNavigation<PlanOfSelectedProject>();
            containerRegistry.RegisterForNavigation<Enviroment>();
            containerRegistry.RegisterForNavigation<Stakholders>();
            containerRegistry.RegisterForNavigation<Team>();
            containerRegistry.RegisterForNavigation<WBS>();
            containerRegistry.RegisterForNavigation<RACI>();
            containerRegistry.RegisterDialog<EditTaskProperties, EditTaskPropertiesViewModel>();
        }
    }
}