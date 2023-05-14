using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Models
{
    public class DayModel
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int BreakFast_Id { get; set; }
        public int Lunch_Id { get; set; }
        public int Dinner_Id { get; set; }
        public int Week_Id { get; set; }
    }
}
