using FoodCalculator.Model;
using FoodCalculator.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Stores
{
    public class DataStore
    {
        private readonly DB_Operator db_Operator;
        private ObservableCollection<Food> FoodList { get; set; } = new();
        private ObservableCollection<string> FoodTypes { get; set; } = new();
        public ObservableCollection<DayTemplate> DayTemplates { get; set; } = new ObservableCollection<DayTemplate>();
        public List<Day> Days { get; set; } = new();
        public List<MealFilling> Meals { get; set; }
        public Week CurrentWeek { get; set; }
        public DataStore(DB_Operator Operator) 
        {
            db_Operator = Operator;
            GetFoodListFromDB();
            GetFoodTypesFromDBAsync();
            GetDaysFromDB();
            GetSettingsFromDB();
            GetCurrentWeekFromDB();
        }
        public ObservableCollection<string> GetFoodTypes()
        {
            return FoodTypes;
        }
        public ObservableCollection<Food> GetFoodList()
        {
            return FoodList;
        }
        
        private void GetFoodListFromDB()
        {
            var fl= GetAllFoodFromDBAsync();
            foreach (var food in fl.Result)
                FoodList.Add(food);
        }
        private async Task<IEnumerable<Food>> GetAllFoodFromDBAsync()
        {
            return await db_Operator.GetAllFood();
        }
        private async Task GetFoodTypesFromDBAsync()
        {
            var ft = await db_Operator.GetFoodTypes();
            foreach (var foodtype in ft)
            {
                FoodTypes.Add(foodtype);
            }            
        }
        
        
        private void GetCurrentWeekFromDB()
        {
            CurrentWeek=null;
        }
        private void GetDaysFromDB()
        {
            var daylist = GetDaysFromDBAsync();
            foreach (var day in daylist.Result)
                Days.Add(day);

        }
        private async Task<IEnumerable<Day>> GetDaysFromDBAsync()
        {
            return await db_Operator.GetDays(Meals);
            
        }
        public async Task UpdateSettingsInDB()
        {
            await db_Operator.UpdateSettings(DayTemplates.ToList());
        }
        public async Task GetSettingsFromDB()
        {
            var set = await db_Operator.GetSettings();
            foreach (var setting in set)
                DayTemplates.Add(setting);
        }
        public async Task AddType(string type)
        {
            await db_Operator.InsertFoodType(type);
            FoodTypes.Add(type);
        }
        public async Task AddFood(Food food)
        {
            food.Id=db_Operator.GetCurrentFoodId();
            await db_Operator.InsertFood(food);            
            FoodList.Add(food);
        }
        public async Task RemoveFood(Food food) 
        {
            await db_Operator.RemoveFood(food);
            FoodList.Remove(food);
        }
        
    }
}
