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
    internal class Week : INotifyPropertyChanged
    {
        public DateOnly FirstDay { get; set; }
        public DateOnly LastDay { get; set; }
        //public string Date { get { return _date; } set { _date = value ?? ""; OnPropertyChanged("Date"); } }
        //public string _date = null!;
        public ObservableCollection<Food> Breakfasts { get; set; } = new ObservableCollection<Food> { new Food(), new Food(), new Food(), new Food(), new Food(), new Food(), new Food() };
        public ObservableCollection<MealFilling> Lunches { get; set; } = new ObservableCollection<MealFilling> { new MealFilling(), new MealFilling(), new MealFilling(), new MealFilling(), new MealFilling(), new MealFilling(), new MealFilling() };
        public ObservableCollection<MealFilling> Dinners { get; set; } = new ObservableCollection<MealFilling> { new MealFilling(), new MealFilling(), new MealFilling(), new MealFilling(), new MealFilling(), new MealFilling(), new MealFilling() };
        public int Id { get { return id; } set { if (value >= 0) { id = value; OnPropertyChanged("Id"); } } }
        private int id;
        public void FillBreakfast(List<Food> foods)
        {            
            for (int i = 0; i < 7||i<foods.Count; i++)
            {
                Breakfasts[i] = foods[i];
            }
        }
        public void FillLunchesAndDinners(List<Food> main, List<Food> garn, List<Food> salorsoup)
        {            
            for (int i = 0; i < 7; i++)
            {
                Lunches[i] = new MealFilling(main[i * 2], garn[i * 2], salorsoup[i * 2]);                
            }
            for (int i = 1; i < 8; i++)
            {
                Dinners[i-1] = new MealFilling(main[i * 2-1], garn[i * 2-1], salorsoup[i * 2 - 1]);
            }
        }
        (DateOnly, DateOnly) FirstAndLastDay()
        {
            DateOnly first = DateOnly.FromDateTime(DateTime.Now);
            DateOnly last = DateOnly.FromDateTime(DateTime.Now);
            while (first.DayOfWeek != DayOfWeek.Monday)
            {
                first = first.AddDays(-1);
            }
            while (last.DayOfWeek != DayOfWeek.Sunday)
            {
                last = last.AddDays(1);
            }
            return (first, last);
        }
        public bool IsFilled()
        {
            if (Breakfasts.Count == 7 && Lunches.Count == 7 && Dinners.Count == 7) { return true; }
            return false;
        }        
        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public class MealFilling : INotifyPropertyChanged
        {
            public int Id { get { return id; } set { if (value >= 0) { id = value; OnPropertyChanged("Id"); } } }
            private int id;
            public Food Main { get { return _main; } set { _main = value; OnPropertyChanged("Main"); } }
            private Food _main = null!;
            public Food Garnish { get { return _garnish; } set { _garnish = value; OnPropertyChanged("Garnish"); } }
            private Food _garnish = null!;
            public Food SaladOrSoup { get { return _saladOrSoup; } set { _saladOrSoup = value; OnPropertyChanged("SaladOrSoup"); } }
            private Food _saladOrSoup = null!;
            public string Name { get { return _name; } set { _name = value ?? ""; OnPropertyChanged("Name"); } }
            private string _name = "";
            public event PropertyChangedEventHandler? PropertyChanged = null!;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
            public MealFilling(Food main, Food garnish, Food saladOrSoup)
            {
                Main = main;
                Garnish = garnish;
                SaladOrSoup = saladOrSoup;
                Name = $"{main.Name} + {garnish.Name} + {saladOrSoup.Name}"; 
            }
            public MealFilling() { }
        }
    }
}
