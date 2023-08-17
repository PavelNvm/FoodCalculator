using FoodCalculator.Model;
using System;
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
        private ObservableCollection<Food> FoodList { get; set; } = new();
        private ObservableCollection<string> FoodTypes { get; set; }
        public ObservableCollection<DayTemplate> DayTemplates { get; set; }

        public DataStore() 
        {
            GetFoodListFromDB();
            GetFoodTypesFromDB();
            GetSettedTypesFromDB();
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
            popa();
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
        public void AddFood(Food food)
        {
            FoodList.Add(food);
        }
        public void RemoveFood(Food food) 
        {
            FoodList.Remove(food);
        }
        void popa()
        {
            FoodList.Add(new Food() { Id = 0, Name = "kaWa", Type = "raz" });
            FoodList.Add(new Food() { Id = 1, Name = "eggs", Type = "raz" });
            FoodList.Add(new Food() { Id = 2, Name = "kotleta", Type = "dva", Portions = 3 });
            FoodList.Add(new Food() { Id = 3, Name = "seledka pod shuboi", Type = "dva" });
            FoodList.Add(new Food() { Id = 4, Name = "jarennaya chicken", Type = "dva", Portions = 4 });
            FoodList.Add(new Food() { Id = 5, Name = "Winters salad", Type = "Salad" });
            //FoodList.Add(new Food() { Id = 6, Name = "rice", Type = "Garnish", Portions = 2 });
            //FoodList.Add(new Food() { Id = 7, Name = "kartoxa", Type = "Garnish", Portions = 2 });
            //FoodList.Add(new Food() { Id = 8, Name = "borsh", Type = "Soup", Portions = 3 });
            //FoodList.Add(new Food() { Id = 9, Name = "1", Type = "KaWa" });
            //FoodList.Add(new Food() { Id = 10, Name = "2", Type = "Eggs" });
            //FoodList.Add(new Food() { Id = 11, Name = "k", Type = "Main", Portions = 3 });
            //FoodList.Add(new Food() { Id = 12, Name = "eledka pod shuboi", Type = "Salad" });
            //FoodList.Add(new Food() { Id = 13, Name = "arennaya chicken", Type = "Main", Portions = 4 });
            //FoodList.Add(new Food() { Id = 14, Name = "inters salad", Type = "Salad" });
            //FoodList.Add(new Food() { Id = 15, Name = "ice", Type = "Garnish", Portions = 2 });
            //FoodList.Add(new Food() { Id = 16, Name = "artoxa", Type = "Garnish", Portions = 2 });
            //FoodList.Add(new Food() { Id = 17, Name = "orsh", Type = "Soup", Portions = 3 });
        }
    }
}
