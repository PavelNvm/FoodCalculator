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
        private ObservableCollection<string> FoodTypes { get; set; }
        public ObservableCollection<DayTemplate> DayTemplates { get; set; }
        public Week CurrentWeek { get; set; }
        public DataStore(DB_Operator Operator) 
        {
            db_Operator = Operator;
            GetFoodListFromDB();
            GetFoodTypesFromDB();
            GetSettedTypesFromDB();
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
        public async Task<IEnumerable<Food>> GetAllFoodFromDBAsync()
        {
            return await db_Operator.GetAllFood();
        }
        private void GetFoodListFromDB()
        {
            var fl= GetAllFoodFromDBAsync();
            foreach (var food in fl.Result)
                FoodList.Add(food);
            //popa();
        }
        private void GetFoodTypesFromDB()
        {
            FoodTypes = new ObservableCollection<string>() {"raz","dva" };
        }
        private void GetSettedTypesFromDB()
        {
            DayTemplates = new ObservableCollection<DayTemplate>();
            for(int i = 0; i < 7; i++)
            {
                DayTemplates.Add(new DayTemplate("raz", "raz", "raz"));
            }
        }
        private void GetCurrentWeekFromDB()
        {
            CurrentWeek=null;
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
