using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModels
{
   public class Precedessor : INotifyPropertyChanged
    {
        public ProjectTask Task { get; set; }
        int _offset;
        public int Offset {
            get { return _offset; }
            set
            {                
                if (value != _offset)
                {
                    int temp = _offset;
                    try
                    {
                        _offset = value;
                        Task.RefreshSuccessors();
                    }
                    catch
                    {

                    }
                    
                 //   Task.
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
  
        public Precedessor()
        {
            Task = new ProjectTask();
            Offset = 0;
        }
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
