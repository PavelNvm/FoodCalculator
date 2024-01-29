using FoodCalculator.DB_Contexts;
using FoodCalculator.DbContexts;
using FoodCalculator.DTOs;
using FoodCalculator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Services
{
    public class DB_Operator
    {
        private readonly FoodCalculatorDbContextFactory _dbContextFactory;
        private int currentFoodID;
        public int GetCurrentFoodId()
        {
            return currentFoodID;
        }
        public DB_Operator(FoodCalculatorDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _GetCurrentFoodId();
        }
        private async void _GetCurrentFoodId()
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                FoodDTO a = ToFoodDTO(new Food() { Name="a",Type="a"});
                context.FoodList.Add(a);
                await context.SaveChangesAsync();
                currentFoodID = a.ID;
                context.FoodList.Remove(a);
                await context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Food>> GetAllFood()
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<FoodDTO> foodDTOs = await context.FoodList.ToListAsync();
                return foodDTOs.Select(r => ToFood(r));
            }
        }
        public async Task InsertFood(Food food)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                FoodDTO foodDTO = ToFoodDTO(food);
                context.FoodList.Add(foodDTO);
                await context.SaveChangesAsync();
                currentFoodID++;
            }
        }
        public async Task RemoveFood(Food food)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                FoodDTO foodDTO = ToFoodDTO(food);
                context.FoodList.Remove(foodDTO);
                await context.SaveChangesAsync();
            }
        }

        private FoodDTO ToFoodDTO(Food food)
        {
            return new FoodDTO()
            { ID = food.Id, Name = food.Name, FoodType = food.Type, Portions = food.Portions, Modifier = food.Modifier };
        }
        private static Food ToFood(FoodDTO r)
        {
            return new Food(r.ID, r.Name, r.FoodType, r.Modifier, r.Portions);
        }
    }
}
