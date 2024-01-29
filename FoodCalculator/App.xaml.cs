using FoodCalculator.DB_Contexts;
using FoodCalculator.DbContexts;
using FoodCalculator.Services;
using FoodCalculator.Stores;
using FoodCalculator.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FoodCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=FoodCalculator.db";
        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            FoodCalculatorDbContextFactory DbContextFactory = new FoodCalculatorDbContextFactory(CONNECTION_STRING);
            using (FoodCalculatorDbContext dbContext = new FoodCalculatorDbContext(options))
            {
                dbContext.Database.Migrate();
            }
            NavigationStore navigationStore = new NavigationStore();
            DataStore dataStore = new DataStore(new DB_Operator(DbContextFactory) );
            navigationStore.CurrentViewModel = new CalculatorViewModel(navigationStore, dataStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}