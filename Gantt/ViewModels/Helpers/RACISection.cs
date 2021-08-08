using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.ViewModels.Helpers
{
  public  class RACISection : ProjectPlanSection
    {
      public  RACISection(IRegionManager _regionManager) : base(_regionManager)
        {

        }
        public override void ActivateSection()
        {
            regionManager.RequestNavigate("SelectedProjectRegion", "RACI");
        }
    }
}
