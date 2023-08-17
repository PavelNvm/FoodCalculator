using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    public class Calculator
    {
        private List<Food> FoodList;
        private List<string> FoodTypes;
        private Food[][] SortedFoods;
        ObservableCollection<DayTemplate> DayTemplates { get; set; }
        private Week WeekForCalculating;
        public Calculator(Week week,IEnumerable<Food> foodlist, ObservableCollection<DayTemplate> dayTemplates, IEnumerable<string> foodtypes)
        {
            FoodList = foodlist.ToList();
            DayTemplates = dayTemplates;
            WeekForCalculating = week;
            FoodTypes = foodtypes.ToList();
            SortedFoods = new Food[FoodTypes.Count()][];
            for (int i = 0; i < SortedFoods.Length; i++)
            {
                var temp = from fl in FoodList
                           where fl.Type == FoodTypes[i]
                           select fl;
                SortedFoods[i] = new Food[temp.Count()];
                SortedFoods[i] = temp.ToArray();
            }
        }
        public void Calculate()
        {
            Random rnd = new Random();
            WeekForCalculating.ClearAllFood();
            
        }
    }
}