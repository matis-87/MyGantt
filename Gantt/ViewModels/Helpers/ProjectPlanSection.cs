using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.ViewModels.Helpers
{
   public abstract class ProjectPlanSection
    {
        internal IRegionManager regionManager;
        public ProjectPlanSection(IRegionManager _regionManager)
        {
            regionManager = _regionManager;
        }
        public abstract void ActivateSection();
    }
}
