using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    public class Day
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public MealFilling BreakFast { get; set; }
        public MealFilling Lunch { get; set; }
        public MealFilling Dinner { get; set; }
        public int Week_Id { get; set; }
    }
}
