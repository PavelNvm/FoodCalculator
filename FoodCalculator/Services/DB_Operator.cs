using FoodCalculator.DB_Contexts;
using FoodCalculator.DbContexts;
using FoodCalculator.DTOs;
using FoodCalculator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
                FoodDTO a = ToFoodDTO(new Food() { Name = "a", Type = "a" });
                context.FoodList.Add(a);
                await context.SaveChangesAsync();
                currentFoodID = a.ID;
                context.FoodList.Remove(a);
                await context.SaveChangesAsync();
            }
        }




        public async Task<IEnumerable<Day>> GetDays(IEnumerable<MealFilling> MFs)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                List<DayDTO> FoodTypeDTOs = await context.Days.ToListAsync();
                List<Day> days = ToDay(FoodTypeDTOs, MFs);
                return days;
            }
        }
        public async Task InsertDays(List<Day> days)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                foreach(Day day in days)
                {
                    context.Days.Add(ToDayDTO(day));

                }
                context.SaveChanges();
            }
        }
        private List<Day> ToDay(List<DayDTO> DayDTO, IEnumerable<MealFilling> MFs)
        {
            List<Day> result = new List<Day>();
            foreach(DayDTO dayDTO in DayDTO)
            {
                result.Add(new Day() { Id = dayDTO.Id, Date = dayDTO.Date, Breakfast = MFs.First(r => r.Id == dayDTO.BreakFast_Id), Lunch = MFs.First(r => r.Id == dayDTO.Lunch_Id), Dinner = MFs.First(r => r.Id == dayDTO.Dinner_Id) ,Week_Id = dayDTO.Week_Id });
            }
            return result;
        }
        private DayDTO ToDayDTO(Day day)
        {
            DayDTO res = new DayDTO() { Id = day.Id, Date = day.Date, BreakFast_Id = day.Breakfast.Id, Lunch_Id = day.Lunch.Id, Dinner_Id = day.Dinner.Id, Week_Id = day.Week_Id };
            return res;
        }

        public async Task<IEnumerable<string?>> GetFoodTypes()
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<FoodTypeDTO> FoodTypeDTOs = await context.FoodTypes.ToListAsync();
                List<string?> types = FoodTypeDTOs.Select(r => r.Name).ToList();
                return types;
            }
        }
        public async Task InsertFoodType(string FoodType)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.FoodTypes.Add(new FoodTypeDTO() { Name = FoodType });
                await context.SaveChangesAsync();
            }
        }
        public async Task RemoveFoodType(string FoodType)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.FoodTypes.Remove(new FoodTypeDTO() { Name = FoodType });
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
        public async Task UpdateSettings(List<DayTemplate> settings)
        {
            if(settings.Count!=7)
            {
                throw new Exception("Setting must contain seven elements");                
            }            
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                List<DayTemplatesDTO> settingsDTO = await context.DayTemplates.ToListAsync();                
                EquateSettings(ToDayTemplatesDTOs(settings), settingsDTO);
                await context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<DayTemplate>> GetSettings()
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                List<DayTemplatesDTO> settings = await context.DayTemplates.ToListAsync();
                if (settings.Count<7)
                {
                    context.DayTemplates.Add(new DayTemplatesDTO() { DayNumber = 1, Breakfast = "", Lunch = "", Dinner = "" });
                    context.DayTemplates.Add(new DayTemplatesDTO() { DayNumber = 2, Breakfast = "", Lunch = "", Dinner = "" });
                    context.DayTemplates.Add(new DayTemplatesDTO() { DayNumber = 3, Breakfast = "", Lunch = "", Dinner = "" });
                    context.DayTemplates.Add(new DayTemplatesDTO() { DayNumber = 4, Breakfast = "", Lunch = "", Dinner = "" });
                    context.DayTemplates.Add(new DayTemplatesDTO() { DayNumber = 5, Breakfast = "", Lunch = "", Dinner = "" });
                    context.DayTemplates.Add(new DayTemplatesDTO() { DayNumber = 6, Breakfast = "", Lunch = "", Dinner = "" });
                    context.DayTemplates.Add(new DayTemplatesDTO() { DayNumber = 7, Breakfast = "", Lunch = "", Dinner = "" });
                    await context.SaveChangesAsync();
                    settings = await context.DayTemplates.ToListAsync();
                }
                return ToDayTemplate(settings);
            }
        }

        
        private static void EquateSettings(List<DayTemplatesDTO> NewSettings, List<DayTemplatesDTO> OldSettings)
        {
            for(int i = 0;i < NewSettings.Count;i++)
            {
                OldSettings[i].Breakfast = NewSettings[i].Breakfast;
                OldSettings[i].Lunch = NewSettings[i].Lunch;
                OldSettings[i].Dinner= NewSettings[i].Dinner;
            }
        }
        private static List<DayTemplatesDTO> ToDayTemplatesDTOs(List<DayTemplate> settings)
        {
            List<DayTemplatesDTO> result = new List<DayTemplatesDTO>();
            foreach (DayTemplate dayTemplate in settings)
            {
                DayTemplatesDTO dayTemplateDTO = new DayTemplatesDTO() {DayNumber = dayTemplate.DayNumber};
                StringBuilder breakfast = new StringBuilder();
                StringBuilder lunch = new StringBuilder();
                StringBuilder dinner = new StringBuilder();
                foreach (StringWrapper element in dayTemplate.Breakfast)
                {
                    breakfast.Append(element.ToString());
                    breakfast.Append(';');
                }
                foreach (StringWrapper element in dayTemplate.Lunch)
                {
                    lunch.Append(element.ToString());
                    lunch.Append(';');
                }
                foreach (StringWrapper element in dayTemplate.Dinner)
                {
                    dinner.Append(element.ToString());
                    dinner.Append(';');
                }
                dayTemplateDTO.Breakfast=breakfast.ToString();
                dayTemplateDTO.Lunch=lunch.ToString();
                dayTemplateDTO.Dinner=dinner.ToString();                
                result.Add(dayTemplateDTO);
            }
            return result;
        }
        private static List<DayTemplate> ToDayTemplate(List<DayTemplatesDTO> settingDTO)
        {
            List<DayTemplate> result = new List<DayTemplate>();
            
            foreach (DayTemplatesDTO dayTemplate in settingDTO)
            {
                ObservableCollection<StringWrapper> Breakfast = new ObservableCollection<StringWrapper>();
                ObservableCollection<StringWrapper> Lunch = new ObservableCollection<StringWrapper>();
                ObservableCollection<StringWrapper> Dinner = new ObservableCollection<StringWrapper>();
                foreach (string foodtype in dayTemplate.Breakfast.Split(';'))
                {
                    Breakfast.Add(new StringWrapper(foodtype));
                }
                Breakfast.RemoveAt(Breakfast.Count()-1);
                foreach (string foodtype in dayTemplate.Lunch.Split(';'))
                {
                    Lunch.Add(new StringWrapper(foodtype));
                }
                Lunch.RemoveAt(Lunch.Count() - 1);
                foreach (string foodtype in dayTemplate.Dinner.Split(';'))
                {
                    Dinner.Add(new StringWrapper(foodtype));
                }
                Dinner.RemoveAt(Dinner.Count() - 1);
                result.Add(new DayTemplate() {DayNumber=dayTemplate.DayNumber, Breakfast = Breakfast,Lunch=Lunch,Dinner=Dinner });
                
            }


            return result;
        }
    }
}
