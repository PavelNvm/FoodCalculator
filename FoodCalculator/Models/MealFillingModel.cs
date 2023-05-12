using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Models
{
    public class MealFillingModel
    {
        public int ID { get; set; }
        public int MF_TypeID { get; set; }//tiny int in DB 0=Breakfast 1=Lunch 2=Dinner
        public string? FoodListOrder { get; set; }
        public int FoodQuantity { get; set; }
        public int Day_Id { get; set; }

    }
}
