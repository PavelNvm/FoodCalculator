using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    public class Day : INotifyPropertyChanged
    {
        public string? Date { get; set; }
        public MealFilling Breakfast { get; set; }
        public MealFilling Lunch { get; set; } 
        public MealFilling Dinner { get; set; } 
        public int Week_Id { get; set; }

        public Day(string date)
        {
            Date = date;
            Breakfast = new MealFilling("Breakfast", Date);
            Lunch = new MealFilling("Lunch", Date);
            Dinner = new MealFilling("Dinner", Date);
        }
        
        internal void ClearAllFood()
        {
            Breakfast.ClearAllFood();
            Lunch.ClearAllFood();
            Dinner.ClearAllFood();
        }
        public void equate(Day day)
        {
            
            Date = day.Date;
            Week_Id = day.Week_Id;            
            Breakfast.equate(day.Breakfast);
            Lunch.equate(day.Lunch);
            Dinner.equate(day.Dinner);

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
