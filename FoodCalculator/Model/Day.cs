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
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public MealFilling Breakfast { get; set; } = new MealFilling("Breakfast");
        public MealFilling Lunch { get; set; } = new MealFilling("Lunch");
        public MealFilling Dinner { get; set; } = new MealFilling("Dinner");
        public int Week_Id { get; set; }

        internal void ClearAllFood()
        {
            Breakfast.ClearAllFood();
            Lunch.ClearAllFood();
            Dinner.ClearAllFood();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
