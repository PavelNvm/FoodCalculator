using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FoodCalculator.DTOs
{
    public class DayTemplatesDTO
    {
        [Key]
        public int DayNumber { get; set; }
        public string Breakfast { get;set; }
        public string Lunch { get;set; }
        public string Dinner { get;set; }
    }
}
