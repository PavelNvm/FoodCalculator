using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    public class Calculator
    {
        public List<Food> FoodList = new List<Food>();
        public List<string> FoodTypes = new List<string>();
        public Food[][] SortedFoods;
        public string[][] SetedTypes;
        public Week WeekForCalculating;
        public Calculator(Week week, string[][] setedtypes) 
        {
            SetedTypes = setedtypes;
            WeekForCalculating = week;
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
            for(int i=0; i<7; i++)
            {
                for(int j = 0; j < SetedTypes[i].Length; j++)
                {

                }
            }
        }
    }
}
