using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGantt.ViewModels
{
    public class ProjectPlanViewModel : BindableBase
    {
        public DelegateCommand<int?> MenuCommand { get; private set; }
        public ProjectPlanViewModel()
        {
            MenuCommand = new DelegateCommand<int?>(MenuCmd);
        }
        public void MenuCmd(int? h)
        {
            //     SelectedTask.BrakeRelations();
            //  Lista.Remove(SelectedTask);
            // //  Lista[2].Predecessor = Lista[0];
            int g = 0;
            //  Lista[0] = temp;
        }
    }
}
