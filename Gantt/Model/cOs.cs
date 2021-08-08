using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.Model
{
    public class cOs : INotifyPropertyChanged
    {

        public string Data
        {
            get
            {
                if (((int)Dat.DayOfWeek == 0) || ((int)Dat.DayOfWeek == 6))
                    NonWorkingDay = true;
                else
                    NonWorkingDay = false;
                return DniTygodni[(int)Dat.DayOfWeek];
            }
        }
        public DateTime Dat { get; set; }
        bool _nonWorkinDay;
        public bool NonWorkingDay
        {
            get { return _nonWorkinDay; }
            set
            {
                if (value != _nonWorkinDay)
                {
                    _nonWorkinDay = value;
                    NotifyPropertyChanged();
                }
            }
        }
        IDictionary<int, string> DniTygodni;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public cOs()
        {
            DniTygodni = new Dictionary<int, string>();
            DniTygodni.Add(0, "Nd");
            DniTygodni.Add(1, "Pn");
            DniTygodni.Add(2, "Wt");
            DniTygodni.Add(3, "Śr");
            DniTygodni.Add(4, "Czw");
            DniTygodni.Add(5, "Pt");
            DniTygodni.Add(6, "Sb");
        }
    }
}
