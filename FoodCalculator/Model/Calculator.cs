using System;
using System.Collections.Generic;
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
        Tuple<string[], string[], string[]>[] SetedTypes;
        private Week WeekForCalculating;
        public Calculator(Week week,IEnumerable<Food> foodlist, Tuple<string[], string[], string[]>[] setedtypes, IEnumerable<string> foodtypes)
        {
            FoodList = foodlist.ToList();
            SetedTypes = setedtypes;
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
            for(int i=0; i<SetedTypes.Length; i++)
            {
                for(int j = 0; j < SetedTypes[i].Item1.Length; j++)
                {
                    string temp = SetedTypes[i].Item1[j];
                    int ind  = FoodTypes.IndexOf(temp);
                    int randomizedindex = rnd.Next(SortedFoods[ind].Length);
                    Food a = SortedFoods[ind][randomizedindex];
                    WeekForCalculating.DaysOfTheWeek[i].BreakFast.FoodList.Add(a);
                }
                for (int j = 0; j < SetedTypes[i].Item2.Length; j++)
                {
                    string temp = SetedTypes[i].Item2[j];
                    int ind = FoodTypes.IndexOf(temp);
                    int randomizedindex = rnd.Next(SortedFoods[ind].Length);
                    Food a = SortedFoods[ind][randomizedindex];
                    WeekForCalculating.DaysOfTheWeek[i].Lunch.FoodList.Add(a);
                }
                for (int j = 0; j < SetedTypes[i].Item3.Length; j++)
                {
                    string temp = SetedTypes[i].Item3[j];
                    int ind = FoodTypes.IndexOf(temp);
                    int randomizedindex = rnd.Next(SortedFoods[ind].Length);
                    Food a = SortedFoods[ind][randomizedindex];
                    WeekForCalculating.DaysOfTheWeek[i].Dinner.FoodList.Add(a);
                }
            }
        }
    }
}