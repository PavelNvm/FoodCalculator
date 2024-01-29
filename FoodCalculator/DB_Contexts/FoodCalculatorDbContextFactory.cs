using FoodCalculator.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.DB_Contexts
{
    public class FoodCalculatorDbContextFactory
    {
        private readonly string _connectionString;

        public FoodCalculatorDbContextFactory(string connectionString)
        {
            _connectionString = connectionString; 
        }
        public FoodCalculatorDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new FoodCalculatorDbContext(options);
        }
    }
}
