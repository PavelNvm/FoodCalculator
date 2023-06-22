using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    public class Week
    {
        public int Id { get; set; }
        public DateOnly FirstDay { get; set; }
        public DateOnly LastDay { get; set; }
        public Day[] DaysOfTheWeek { get; set; } = new Day[7] {new Day(), new Day(), new Day(), new Day(), new Day(), new Day(), new Day() };
        public Week() 
        {
            for(int i =0;i<7;i++)
            {
                DaysOfTheWeek[i].Week_Id = Id;
            }
        }

        internal void ClearAllFood()
        {
            for(int i=0;i<7;i++)
            {
                DaysOfTheWeek[i].ClearAllFood();
            }
        }
    }
}
