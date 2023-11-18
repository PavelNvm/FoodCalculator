using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FoodCalculator.DTOs
{
    public class FoodDTO
    {
        [Key]
        public int ID { get; set; }
        public int FoodTypeID { get; set; }
        public int Portions { get; set; }
        public int Modifier { get; set; }
        public string? Name { get; set; }
    }
}
