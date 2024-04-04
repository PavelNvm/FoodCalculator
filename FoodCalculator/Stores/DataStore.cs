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
        public List<MealFilling> Meals { get; set; } = new List<MealFilling>();
        public Week CurrentWeek { get; set; }
        public Week NextWeek { get; set; }
        public bool IsCurrentWeekInDB { get; set; }
        public bool IsNextWeekInDB { get; set; }
        public (DateOnly,DateOnly) CurrentWeekBorders { get; set; }
        public (DateOnly,DateOnly) NextWeekBorders { get; set; }
        public DataStore(DB_Operator Operator) 
        {
            CurrentWeekBorders = Algorithms.WeekBorders(0);
            NextWeekBorders = Algorithms.WeekBorders(1);
                db_Operator = Operator;
            CheckForWeeksInDB();
            StartAsync();
        }
        private async void StartAsync()
        {
            await GetSettingsFromDB();
            await GetFoodTypesFromDBAsync();
            GetFoodListFromDB();   
            GetCurrentWeekFromDB();
            GetNextWeekFromDB();
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
        public async Task RemoveFoodTypeFromDBAsync(string type)
        {
            await db_Operator.RemoveFoodType(type);
        }
        private void CheckForWeeksInDB()
        {
            (bool,bool) a =db_Operator.CheckForWeeks(CurrentWeekBorders, NextWeekBorders).Result;
            IsCurrentWeekInDB = a.Item1;
            IsNextWeekInDB = a.Item2;

        }
        private void GetCurrentWeekFromDB()
        {
            if (IsCurrentWeekInDB)
            {
                Meals.AddRange( db_Operator.GetMealFillingsByDate(FoodList.ToList(), CurrentWeekBorders).Result.ToList());
                CurrentWeek = new Week(CurrentWeekBorders, db_Operator.GetSpecificDays(Meals, CurrentWeekBorders).Result.ToList());
            }
            else
            CurrentWeek=new Week(CurrentWeekBorders);
        }
        private void GetNextWeekFromDB()
        {
            if (IsNextWeekInDB)
            {
                Meals.AddRange(db_Operator.GetMealFillingsByDate(FoodList.ToList(), NextWeekBorders).Result.ToList());
                NextWeek = new Week(NextWeekBorders, db_Operator.GetSpecificDays(Meals, NextWeekBorders).Result.ToList());
            }
            else
                NextWeek = new Week(NextWeekBorders);
        }
        public async Task InsertWeek(Week week)
        {
            await db_Operator.InsertDays(week);
        }
        private void GetDaysFromDB()
        {
            var daylist = GetDaysFromDBAsync();
            foreach (var day in daylist.Result)
                Days.Add(day);

        }
        private async Task<IEnumerable<Day>> GetDaysFromDBAsync()
        {
            return await db_Operator.GetAllDays(Meals);
            
        }
        public async Task UpdateSettingsInDBAsync()
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
