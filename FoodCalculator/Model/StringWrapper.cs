using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    public class StringWrapper : INotifyPropertyChanged
    {
        public string Value { get { return _value; } set { if (value == null) { _value = ""; } else { _value = value; }; OnPropertyChanged(nameof(Value)); } }
        private string _value;

        public StringWrapper(string value)
        {
            _value = value; 
        }
        public override string ToString()
        {
            return Value;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
