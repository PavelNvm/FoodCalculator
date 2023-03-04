using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator
{
    internal class Week
    {
        List<Food> Breakfasts { get; set; }
        List<(Food Main, Food Garnish, Food SaladOrSoup)> Lunches { get; set; }
        List<(Food Main, Food Garnish, Food SaladOrSoup)> Dinners { get; set; }
        int Id { get; set; }
        
        public void FillBreakfast(List<Food> foods)
        {
            Breakfasts = new List<Food>();
            for (int i = 0; i < 7||i<foods.Count; i++)
            {
                Breakfasts.Add(foods[i]);
            }
        }
        public void FillLunchesAndDinners(List<Food> main, List<Food> garn, List<Food> salorsoup)
        {
            Lunches = new List<(Food, Food, Food)>();
            Dinners = new List<(Food, Food, Food)>();
            for (int i = 0; i < 7; i++)
            {
                Lunches.Add((main[i * 2], garn[i * 2], salorsoup[i * 2]));
            }
            for (int i = 1; i < 8; i++)
            {
                Dinners.Add((main[i * 2 - 1], garn[i * 2 - 1], salorsoup[i * 2 - 1]));
            }
        }
        public bool IsFilled()
        {
            if (Breakfasts.Count == 7 && Lunches.Count == 7 && Dinners.Count == 7) { return true; }
            return false;
        }
    }
}
