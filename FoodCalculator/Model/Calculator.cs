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
        Random rnd = new Random();
        private List<Food> FoodList;
        private List<string> FoodTypes;
        private Food[][] SortedFoods;
        private Queue<Food>[] MultiPortionalFoodQueue;
        private bool[][] Was;
        ObservableCollection<DayTemplate> DayTemplates { get; set; }
        private Week WeekForCalculating;
        public Calculator(Week week,IEnumerable<Food> foodlist, ObservableCollection<DayTemplate> dayTemplates, IEnumerable<string> foodtypes)
        {
            FoodList = foodlist.ToList();
            DayTemplates = dayTemplates;
            WeekForCalculating = week;
            FoodTypes = foodtypes.ToList();
            SortedFoods = new Food[FoodTypes.Count()][];
            MultiPortionalFoodQueue = new Queue<Food>[FoodTypes.Count()];
            for (int i = 0; i < SortedFoods.Length; i++)
            {
                MultiPortionalFoodQueue[i] = new();
                var temp = from fl in FoodList
                           where fl.Type == FoodTypes[i]
                           select fl;
                SortedFoods[i] = new Food[temp.Count()];
                SortedFoods[i] = temp.ToArray();
            }
        }
        public void Calculate()
        {           
            WeekForCalculating.ClearAllFood();
            for(int i = 0;i<7;i++)
            {
                CreateListOfFoods(DayTemplates[i].Breakfast, WeekForCalculating.DaysOfTheWeek[i].BreakFast);
                CreateListOfFoods(DayTemplates[i].Lunch, WeekForCalculating.DaysOfTheWeek[i].Lunch);
                CreateListOfFoods(DayTemplates[i].Dinner, WeekForCalculating.DaysOfTheWeek[i].Dinner);
            }
            
        }
        private void CreateListOfFoods(ObservableCollection<StringWrapper> listoftypes,MealFilling mf)
        {
            List<Food> foodsForMF = new List<Food>();
            foreach (var type in listoftypes)
            {
                int typeindex = FoodTypes.IndexOf(type.Value);
                int foodindex = rnd.Next(SortedFoods[typeindex].Length);
                if (SortedFoods[typeindex][foodindex].Portions==1)
                {
                    foodsForMF.Add(SortedFoods[typeindex][foodindex]);
                }
            }

            InputMF(mf, foodsForMF);


        }
        private void InputMF(MealFilling mf,IEnumerable<Food> foods)
        {
            foreach (var food in foods)
            {
                mf.AddFood(food);
            }
            mf.UpdateOutputValue();
        }
    }
}