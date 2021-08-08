using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gantt.Model;

namespace Gantt.ViewModels.Helpers
{
   public interface IPlanSectionsFactory
    {
        ProjectPlanSection MakeSection(PossiblePlanSections RequestesSectionType);
    }
}
