using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FoodCalculator.DTOs
{
    public class MealFillingDTO
    {
        [Key]
        public int ID { get; set; }
        public string MF_Type { get; set; }
        public string? FoodListOrder { get; set; }
        public int FoodQuantity { get; set; }
        public string Day_Date { get; set; }

    }
}
