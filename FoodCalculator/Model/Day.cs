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
        public MealFilling BreakFast { get; set; } = new MealFilling("BreakFast");
        public MealFilling Lunch { get; set; } = new MealFilling("Lunch");
        public MealFilling Dinner { get; set; } = new MealFilling("Dinner");
        public int Week_Id { get; set; }
    }
}
