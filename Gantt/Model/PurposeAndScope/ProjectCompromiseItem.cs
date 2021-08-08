using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.Model.PurposeAndScope
{
 public   class ProjectCompromiseItem
    {
        public string Name { get; set; }
        public bool isFixed { get; set; }
        public bool isOptimized { get; set; }
        public bool isNegotiated { get; set; }
        public string Comments { get; set; }
    }
}
