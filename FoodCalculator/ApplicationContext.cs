using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator
{
    internal class FoodContext : DbContext
    {
        public DbSet<Food> FoodList { get; set; } = null!;
        //public DbSet<StatRecord> StatRecords { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=FoodDataBase.db");
        }

    }
    internal class WeekContext : DbContext
    {
        public DbSet<Week> WeekList { get; set; } = null!;
        //public DbSet<StatRecord> StatRecords { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WeekDataBase.db");
        }

    }

}
