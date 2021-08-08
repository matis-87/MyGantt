using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGantt.Model
{
  public  class Predecessor:GantTask
    {
        private int _lag;

        public int Lag
        {
            get { return _lag; }
            set
            {
                if (value != _lag)
                {
                    _lag = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
