using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gantt.Model.Enviroment
{
   public class InternalEnviromentItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        List<string> _itemType;
        public List<string> ItemType
        {
            get { return _itemType; }
            set
            {
                _itemType = value;
                NotifyPropertyChanged();
            }
        }
        string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                NotifyPropertyChanged();
            }
        }
        public InternalEnviromentItem(string name)
        {
            Name = name;
            ItemType = new List<string>
            {
                "Mocna strona",
                "Słaba strona"
            };
        }

    }
}
