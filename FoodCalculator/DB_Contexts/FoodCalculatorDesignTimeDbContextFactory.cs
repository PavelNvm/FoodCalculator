using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.DbContexts
{
    public class FoodCalculatorDesignTimeDbContextFactory : IDesignTimeDbContextFactory<FoodCalculatorDbContext>
    {
        public FoodCalculatorDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=FoodCalculator.db").Options;
            return new FoodCalculatorDbContext(options);
        }
    }
}
