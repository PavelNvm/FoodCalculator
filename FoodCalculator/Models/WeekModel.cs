using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Models
{
    public class Week
    {
        public int Id { get; set; }
        public DateOnly FirstDay { get; set; }
        public DateOnly LastDay { get; set; }
    }
}
