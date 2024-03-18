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
        public int DayNumber{get;set;}
        public ObservableCollection<StringWrapper> Breakfast { get;set; } = new ObservableCollection<StringWrapper>();
        public ObservableCollection<StringWrapper> Lunch { get; set; } = new ObservableCollection<StringWrapper>();
        public ObservableCollection<StringWrapper> Dinner { get;set; } = new ObservableCollection<StringWrapper>();
        Random rnd = new Random();

        public DayTemplate() 
        {            
        }
        public DayTemplate(string a,string b, string c)//TODO strange constructor
        {
            Breakfast.Add(new StringWrapper(a));
            Breakfast.Add(new StringWrapper(b));
            Breakfast.Add(new StringWrapper(c));
            Lunch.Add(new StringWrapper(a));
            Lunch.Add(new StringWrapper(b));
            Lunch.Add(new StringWrapper(c));
            Dinner.Add(new StringWrapper(a));
            Dinner.Add(new StringWrapper(b));
            Dinner.Add(new StringWrapper(c));
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
