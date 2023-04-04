using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Food> FoodList { get; set; } = null!;
        public DbSet<Week> WeekList { get; set; } = null!;
        public DbSet<Day> DayList { get; set; } = null!;
        public DbSet<MealFilling> MealFillingList { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=FoodCalcDataBase.db");
        }        
    }
}
