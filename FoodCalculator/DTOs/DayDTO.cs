using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FoodCalculator.DTOs
{
    public class DayDTO
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int BreakFast_Id { get; set; }
        public int Lunch_Id { get; set; }
        public int Dinner_Id { get; set; }
        public int Week_Id { get; set; }
    }
}
