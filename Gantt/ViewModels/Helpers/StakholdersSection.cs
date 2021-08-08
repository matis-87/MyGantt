using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.ViewModels.Helpers
{
    class StakholdersSection : ProjectPlanSection
    {
        public StakholdersSection(IRegionManager _regionManager) : base(_regionManager)
        {

        }
        public override void ActivateSection()
        {
            regionManager.RequestNavigate("SelectedProjectRegion", "Stakholders");
        }
    }
}
