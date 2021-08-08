using Gantt.Model.Enviroment;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gantt.ViewModels
{
    public class EnviromentViewModel : BindableBase
    {
        List<InternalEnviromentItem> _internalEnviroment;
        public List<InternalEnviromentItem> InternalEnviroment
        {
            get { return _internalEnviroment; }
            set { SetProperty(ref _internalEnviroment, value); }
        }
        public EnviromentViewModel()
        {
            InternalEnviroment = new List<InternalEnviromentItem>
            {
                new InternalEnviromentItem("jeden"),
                new InternalEnviromentItem("dwa")

            };
            int h = 0;
        }
    }
}
