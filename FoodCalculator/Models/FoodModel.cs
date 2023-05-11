using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Models
{
    public class FoodModel
    {
        public int ID { get; set; }
        public int FoodTypeID { get; set; }
        public int Portions { get; set; }
        public int Modifier { get; set; }
    }
}
