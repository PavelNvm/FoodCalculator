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
        public string[][] types = new string[7][];
        public Week Week = new Week();
        public Calculator() 
        {
            SortedFoods = new Food[FoodTypes.Count()][];
        }
        public void Calculate()
        {
            for(int i=0; i<7; i++)
            {

            }
        }
    }
}
