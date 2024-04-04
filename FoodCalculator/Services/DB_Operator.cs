using FoodCalculator.DB_Contexts;
using FoodCalculator.DbContexts;
using FoodCalculator.DTOs;
using FoodCalculator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;
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
        private Week CurrentWeek;
        public bool IsCurrentWeek { get; set; } = false;
        private Week NextWeek;
        public bool IsNextWeek { get; set; } = false;
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

        #region MealFillings

        public async Task<IEnumerable<MealFilling>> GetMealFillings(List<Food> foodList)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MealFillingDTO> MFDTOs = await context.MealFillings.ToListAsync();
                return MFDTOs.Select(r => ToMealFilling(r,foodList));
            }
        }
        public async Task<IEnumerable<MealFilling>> GetMealFillingsByDate(List<Food> foodList, (DateOnly, DateOnly) week)
        {

            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                List<MealFillingDTO> MFDTOs = new List<MealFillingDTO>();
                for (DateOnly date = week.Item1; date <= week.Item2; date = date.AddDays(1))
                {
                    var a = context.MealFillings.Where(r => r.Day_Date == date.ToString()).ToListAsync().Result;
                    if (a.Count > 0)
                        MFDTOs.AddRange(a);
                }                
                return MFDTOs.Select(r => ToMealFilling(r, foodList));
            }
        }
        public async Task InsertMealFillnigs(List<MealFilling> mfs)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                foreach (var f in mfs)
                {
                    context.MealFillings.Add(ToMealFillingDTO(f));
                }
                await context.SaveChangesAsync();
            }
        }
        public async Task RemoveMealFillnigs(List<MealFilling> mf)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                foreach (var f in mf)
                {
                    context.MealFillings.Remove(ToMealFillingDTO(f));
                }
                await context.SaveChangesAsync();
            }
        }
        private MealFilling ToMealFilling(MealFillingDTO mfdto,List<Food> foodList)
        {
            MealFilling result = new MealFilling(mfdto.MF_Type, mfdto.Day_Date) { Day_Date=mfdto.Day_Date,Id=mfdto.ID};
            List<int> Ids = new List<int>();
            foreach (string a in mfdto.FoodListOrder.Split(';').ToList())
                if (a != "")
                    Ids.Add(int.Parse(a));
            
            foreach(int id in Ids)
            {
                result.AddFood(foodList.FirstOrDefault(f=>f.Id==id));
            }   
            return result;
        }
        private MealFillingDTO ToMealFillingDTO(MealFilling mf)
        {
            MealFillingDTO result = new MealFillingDTO() { ID=mf.Id,Day_Date=mf.Day_Date,MF_Type=mf.Type};
            StringBuilder order = new StringBuilder();
            foreach(Food f in mf.FoodList)
            {
                order.Append(f.Id);
                order.Append(';');
            }
            result.FoodListOrder=order.ToString();
            return result;
        }
        #endregion

        #region Days
        public async Task<(bool,bool)> CheckForWeeks((DateOnly, DateOnly) curweek, (DateOnly, DateOnly) nextweek)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                List<DayDTO> current = new List<DayDTO>();
                List<DayDTO> next = new List<DayDTO>();
                //List<DayDTO> current = context.Days.Where(r => DateOnly.Parse(r.Date) >= curweek.Item1 && DateOnly.Parse(r.Date) <= curweek.Item2).ToList();
                //List<DayDTO> next = context.Days.Where(r => DateOnly.Parse(r.Date) >= nextweek.Item1 && DateOnly.Parse(r.Date) <= nextweek.Item2).ToList();
                for (DateOnly date=curweek.Item1;date<=curweek.Item2;date = date.AddDays(1))
                {
                    var a =context.Days.Where(r => r.Date == date.ToString()).ToListAsync().Result;
                    if (a.Count == 1)
                        current.Add(a[0]);
                }
                for (DateOnly date = nextweek.Item1; date <= nextweek.Item2; date = date.AddDays(1))
                {
                    var a = context.Days.Where(r => r.Date == date.ToString()).ToListAsync().Result;
                    if (a.Count == 1)
                        next.Add(a[0]);
                }
                if (current.Count == 7)
                {
                    IsCurrentWeek = true;
                    List<MealFilling> CurMFs = GetMealFillingsByDate(GetAllFood().Result.ToList(), curweek).Result.ToList();
                    CurrentWeek = new Week(curweek, ToDay(current, CurMFs));
                     
                }
                if (next.Count == 7)
                {
                    IsNextWeek = true;
                }


                return (current.Count == 7, next.Count == 7);
                
            }
        }

        public async Task<IEnumerable<Day>> GetSpecificDays(IEnumerable<MealFilling> MFs, (DateOnly, DateOnly) week)
        {
            using(FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                List<DayDTO> FoodTypeDTOs=new List<DayDTO>();
                for (DateOnly date = week.Item1; date <= week.Item2; date = date.AddDays(1))
                {
                    var a = context.Days.Where(r => r.Date == date.ToString()).ToListAsync().Result;
                    if (a.Count == 1)
                        FoodTypeDTOs.Add(a[0]);
                }
                List<Day> days = ToDay(FoodTypeDTOs, MFs);
                return days;
            }
        }  
        public async Task<IEnumerable<Day>> GetAllDays(IEnumerable<MealFilling> MFs)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                List<DayDTO> FoodTypeDTOs = await context.Days.ToListAsync();
                List<Day> days = ToDay(FoodTypeDTOs, MFs);
                return days;
            }
        }
        public async Task InsertDays(Week week)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                if(IsCurrentWeek&&week.FirstDay==CurrentWeek.FirstDay)
                {
                    await RewriteDays(week.DaysOfTheWeek.ToList());
                }
                else if(IsNextWeek&&week.FirstDay==NextWeek.FirstDay)
                {
                    await RewriteDays(week.DaysOfTheWeek.ToList());
                }
                else
                {
                    foreach (Day day in week.DaysOfTheWeek.ToList())
                    {
                        context.Days.Add(ToDayDTO(day));
                        await InsertMealFillnigs(new List<MealFilling>() { day.Breakfast, day.Lunch, day.Dinner });
                    }
                    await context.SaveChangesAsync();
                }                
            }
        }
        public async Task RewriteDays(List<Day> days)
        {
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {
                DayDTO tempDayDTO;
                foreach (Day day in days)
                {
                    tempDayDTO = context.Days.First(f=>f.Date==day.Date);
                    context.MealFillings.Where(m=>m.Day_Date==day.Date).ExecuteDelete();
                    await InsertMealFillnigs(new List<MealFilling>() { day.Breakfast, day.Lunch, day.Dinner });
                    equateDayDTO(tempDayDTO, ToDayDTO(day));
                }
                await context.SaveChangesAsync();
            }

        }
        private void equateDayDTO(DayDTO oldDay, DayDTO newDay)
        {            
            oldDay.Date = newDay.Date;
            oldDay.BreakFast_Id = newDay.BreakFast_Id;
            oldDay.Lunch_Id = newDay.Lunch_Id;
            oldDay.Dinner_Id = newDay.Dinner_Id;
            oldDay.Week_Id = newDay.Week_Id;

        }
        private List<Day> ToDay(List<DayDTO> DayDTO, IEnumerable<MealFilling> MFs)
        {
            List<Day> result = new List<Day>();
            foreach(DayDTO dayDTO in DayDTO)
            {
                result.Add(new Day(dayDTO.Date) { Breakfast = MFs.First(r => r.Day_Date == dayDTO.Date && r.Type == "Breakfast"), Lunch = MFs.First(r => r.Day_Date == dayDTO.Date && r.Type == "Lunch"), Dinner = MFs.First(r => r.Day_Date == dayDTO.Date && r.Type == "Dinner"), Week_Id = dayDTO.Week_Id });
            }
            return result;
        }
        private DayDTO ToDayDTO(Day day)
        {
            DayDTO res = new DayDTO() { Date = day.Date, BreakFast_Id = day.Breakfast.Id, Lunch_Id = day.Lunch.Id, Dinner_Id = day.Dinner.Id, Week_Id = day.Week_Id };
            return res;
        }
        #endregion

        #region Foodtypes
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
        #endregion

        #region Food
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
        #endregion 

        #region Settings
        public async Task UpdateSettings(List<DayTemplate> settings)
        {
            if(settings.Count!=7)
            {
                throw new Exception("Setting must contain seven elements");                
            }            
            using (FoodCalculatorDbContext context = _dbContextFactory.CreateDbContext())
            {                
                context.DayTemplates.Where(r=>r.DayNumber<10).ExecuteDelete();
                await context.DayTemplates.AddRangeAsync(ToDayTemplatesDTOs(settings));
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
                    settings.Add(new DayTemplatesDTO() { DayNumber = 1, Breakfast = "Breakfast", Lunch = "Soup;Second;Garnish;", Dinner = "Second;Garnish;" });
                    settings.Add(new DayTemplatesDTO() { DayNumber = 2, Breakfast = "Breakfast", Lunch = "Soup;Second;Garnish;", Dinner = "Second;Garnish;" });
                    settings.Add(new DayTemplatesDTO() { DayNumber = 3, Breakfast = "Breakfast", Lunch = "Soup;Second;Garnish;", Dinner = "Second;Garnish;" });
                    settings.Add(new DayTemplatesDTO() { DayNumber = 4, Breakfast = "Breakfast", Lunch = "Soup;Second;Garnish;", Dinner = "Second;Garnish;" });
                    settings.Add(new DayTemplatesDTO() { DayNumber = 5, Breakfast = "Breakfast", Lunch = "Soup;Second;Garnish;", Dinner = "Second;Garnish;" });
                    settings.Add(new DayTemplatesDTO() { DayNumber = 6, Breakfast = "Breakfast", Lunch = "Soup;Second;Garnish;", Dinner = "Second;Garnish;" });
                    settings.Add(new DayTemplatesDTO() { DayNumber = 7, Breakfast = "Breakfast", Lunch = "Soup;Second;Garnish;", Dinner = "Second;Garnish;" });
                    context.FoodTypes.Add(new FoodTypeDTO() { Name = "Breakfast" });
                    context.FoodTypes.Add(new FoodTypeDTO() { Name = "Soup" });
                    context.FoodTypes.Add(new FoodTypeDTO() { Name = "Second" });
                    context.FoodTypes.Add(new FoodTypeDTO() { Name = "Garnish" });
                    await context.SaveChangesAsync();                    
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
        #endregion
    }
}
