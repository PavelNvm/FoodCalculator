using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    class Algorithms
    {
        public static (DateOnly, DateOnly) WeekBorders(int offset)//offset in weeks
        {
            int day = Convert.ToInt32(DateTime.Now.DayOfWeek);
            DateOnly fd = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            fd = fd.AddDays(-day + 1 + 7 * offset);
            DateOnly sd = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            sd = sd.AddDays(7 - day + 7 * offset);


            return (fd, sd);
        }
    }

}
