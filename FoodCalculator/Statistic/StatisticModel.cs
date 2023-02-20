using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator
{
    public class FCStatistic : INotifyPropertyChanged
    {
        public ObservableCollection<StatRecord> StatDataList { get { return _statDataList; } set { _statDataList = value ?? new ObservableCollection<StatRecord>(); OnPropertyChanged("StatDataList"); } }
        private ObservableCollection<StatRecord> _statDataList;
        
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public FCStatistic()
        {
            StatDataList = new ObservableCollection<StatRecord>();
        }
    }
    public class StatRecord : INotifyPropertyChanged
    {
        public enum DayOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public int Id { get { return _id; } set { Id = value; OnPropertyChanged("Id"); } }
        private int _id;
        public string Food { get { return _food; } set { if (value != null) { _food = value; } } }
        private string _food = null!;
        public string FoodType { get { return _foodType; } set { if (value != null) { _foodType = value; } } }
        private string _foodType = null!;
        public DateTime DateTime { get { return _datetime; } set { _datetime = value; } }
        private DateTime _datetime;
        internal StatRecord(Food food, DateTime dateTime)
        {
            Food = food.Name;
            FoodType = food.Type;
            DateTime = dateTime;
        }
    }
}
