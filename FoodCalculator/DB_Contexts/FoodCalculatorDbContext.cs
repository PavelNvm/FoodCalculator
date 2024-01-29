using FoodCalculator.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.DbContexts
{
    public class FoodCalculatorDbContext : DbContext
    {
        public FoodCalculatorDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FoodTypeDTO> FoodTypes { get; set; }
        public DbSet<DayTemplatesDTO> DayTemplates { get; set; }
        public DbSet<FoodDTO> FoodList { get; set; }
        public DbSet<MealFillingDTO> MealFillings { get; set; }
        public DbSet<DayDTO> Days { get; set; }


    }
}
