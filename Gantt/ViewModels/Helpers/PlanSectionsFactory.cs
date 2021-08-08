using Gantt.Model;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.ViewModels.Helpers
{
    class PlanSectionsFactory : IPlanSectionsFactory
    {
        IRegionManager regionManager;
        public PlanSectionsFactory(IRegionManager _regionManager)
        {
            regionManager = _regionManager;
        }
        public ProjectPlanSection MakeSection(PossiblePlanSections RequestesSectionType)
        {
            switch(RequestesSectionType)
            {
                case PossiblePlanSections.GanttChart: return new GanttSection(regionManager);
                case PossiblePlanSections.PurposeAndScope: return new PurposeAndScopeSection(regionManager);
                case PossiblePlanSections.Enviroment: return new EnviromentSection(regionManager);
                case PossiblePlanSections.Stakholders: return new StakholdersSection(regionManager);
                case PossiblePlanSections.Team: return new TeamSection(regionManager);
                case PossiblePlanSections.WBS: return new WBSSection(regionManager);
                case PossiblePlanSections.RACIMatrix: return new RACISection(regionManager);

            }
            return new GanttSection(regionManager);
        }
    }
}
