using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.DTOs
{
    public class FoodTypeDTO
    {
        [Key]
        public string Name { get; set; }
    }
}
 