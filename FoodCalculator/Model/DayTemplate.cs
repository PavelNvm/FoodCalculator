using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    public class DayTemplate : INotifyPropertyChanged
    {
        public ObservableCollection<StringWrapper> Breakfast { get;set; } = new ObservableCollection<StringWrapper>();
        public ObservableCollection<StringWrapper> Lunch { get; set; } = new ObservableCollection<StringWrapper>();
        public ObservableCollection<StringWrapper> Dinner { get;set; } = new ObservableCollection<StringWrapper>();
        Random rnd = new Random();

        public DayTemplate() 
        {            
        }
        public void equate(DayTemplate source)
        {
            Breakfast.Clear();
            Lunch.Clear();
            Dinner.Clear();
            foreach (var item in source.Breakfast) { Breakfast.Add(new StringWrapper(item.Value)); }
            foreach (var item in source.Lunch) { Lunch.Add(new StringWrapper(item.Value)); }
            foreach (var item in source.Dinner) { Dinner.Add(new StringWrapper(item.Value)); }
        }
        public void Random(ObservableCollection<string> foods)
        {
            Breakfast.Clear();
            Lunch.Clear();
            Dinner.Clear();
            for (int i=0;i<rnd.Next(1,7);i++)
            {
                Breakfast.Add(new StringWrapper(foods[rnd.Next(foods.Count)]));
            }
            for (int i = 0; i < rnd.Next(1,7); i++)
            {
                Lunch.Add(new StringWrapper(foods[rnd.Next(foods.Count)]));
            }
            for (int i = 0; i < rnd.Next(1,7); i++)
            {
                Dinner.Add(new StringWrapper(foods[rnd.Next(foods.Count)]));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
