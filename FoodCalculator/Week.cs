using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator
{
    public class Week //модель
    {
        public DateOnly FirstDay { get; set; }
        public DateOnly LastDay { get; set; }
        public int Id { get; set; } = 0;
        public int Breakfast_Id { get; set; }
        public int Lunch_Id { get; set; }
        public int Dinner_Id { get; set; }
        public Week()
        {

        }   
        public Week(Week week)
        {
            FirstDay = week.FirstDay;
            LastDay = week.LastDay;
            Breakfast_Id = week.Breakfast_Id;
            Lunch_Id = week.Lunch_Id;
            Dinner_Id = week.Dinner_Id;
        }
    }
    public class WeekContext : DbContext
    {
        public DbSet<Week> WeekList { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WeekModelDataBase.db");
        }

    }
    public class ShowingWeek
    {
        public ObservableCollection<Food> Breakfasts { get; set; } = new ObservableCollection<Food> { new Food(), new Food(), new Food(), new Food(), new Food(), new Food(), new Food() };
        public ObservableCollection<Food> Lunches { get; set; } = new ObservableCollection<Food>();
        public ObservableCollection<Food> Dinners { get; set; } = new ObservableCollection<Food>();
    }
    public class WeekBL : INotifyPropertyChanged
    {
        private ObservableCollection<Food> FoodList;
        public MealFillingBL MealFillingBL { get; set; } = new MealFillingBL();
        public DateOnly FirstDay { get; set; }
        public DateOnly LastDay { get; set; }
        public static ShowingWeek ShownigWeek { get; set; } = new ShowingWeek();
        public MealFillingContext LunchesDB { get; set; } = new MealFillingContext();
        public WeekContext WeekDB { get; set; } = new WeekContext();
        public ObservableCollection<Food> Breakfasts { get; set; } = new ObservableCollection<Food> { new Food(), new Food(), new Food(), new Food(), new Food(), new Food(), new Food() };
        public ObservableCollection<MealFillingBL> Lunches { get; set; } = new ObservableCollection<MealFillingBL> { new MealFillingBL(), new MealFillingBL(), new MealFillingBL(), new MealFillingBL(), new MealFillingBL(), new MealFillingBL(), new MealFillingBL() };
        public ObservableCollection<MealFillingBL> Dinners { get; set; } = new ObservableCollection<MealFillingBL> { new MealFillingBL(), new MealFillingBL(), new MealFillingBL(), new MealFillingBL(), new MealFillingBL(), new MealFillingBL(), new MealFillingBL() };
        public void FillBreakfast(Week week, List<Food> breakfasts)
        {
            for (int i = 0; i < 7 || i < breakfasts.Count; i++)
            {
                Breakfasts[i] = breakfasts[i];
                week.Breakfast_Id = Breakfasts[i].Id;
                WeekDB.WeekList.Add(week);
            }
        }
        private void FillDB(Week week)
        {
            for (int i = 0; i < 7; i++)
            {
                week.Breakfast_Id = Breakfasts[i].Id;
                week.Lunch_Id = Lunches[i].MealFilling.Id;
                week.Dinner_Id = Dinners[i].MealFilling.Id;
                try
                {
                    WeekDB.WeekList.Add(new Week(week));
                    WeekDB.SaveChanges();
                }
                catch { }
            }

        }
        public void FillLunchesAndDinners(Week week, List<Food> main, List<Food> garn, List<Food> salorsoup)
        {
            for (int i = 0; i < 7; i++)
            {
                Lunches[i] = new MealFillingBL(main[i * 2], garn[i * 2], salorsoup[i * 2], week.Id, "Lunch");
            }
            for (int i = 1; i < 8; i++)
            {
                Dinners[i - 1] = new MealFillingBL(main[i * 2 - 1], garn[i * 2 - 1], salorsoup[i * 2 - 1], week.Id, "Dinner");
            }
            FillDB(week);
        }
        public void FillShowingWeek(Week week)
        {
            FoodList = (Linker.ViewModels.Where(item => item.GetType().Name.Equals("FoodCalcer")).FirstOrDefault() as FoodCalcer).FoodList;
            int i = 0;
            foreach (var element in WeekDB.WeekList.Local.Where(item => item.FirstDay == week.FirstDay && item.LastDay == item.LastDay))
            {
                if (i >= 7)
                    break;
                ShownigWeek.Breakfasts[i] = (FoodList.Where(item => item.Id == element.Breakfast_Id).FirstOrDefault());
                i++;
            }
        }
        (DateOnly, DateOnly) FirstAndLastDay()
        {
            DateOnly first = DateOnly.FromDateTime(DateTime.Now);
            DateOnly last = DateOnly.FromDateTime(DateTime.Now);
            while (first.DayOfWeek != DayOfWeek.Monday)
            {
                first = first.AddDays(-1);
            }
            while (last.DayOfWeek != DayOfWeek.Sunday)
            {
                last = last.AddDays(1);
            }
            return (first, last);
        }
        public bool IsFilled()
        {
            if (Breakfasts.Count >= 7 && Lunches.Count >= 7 && Dinners.Count >= 7)
            {
                return true;
            }
            return false;
        }
        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public WeekBL()
        {
            WeekDB.Database.EnsureCreated();
            WeekDB.WeekList.Load();
        }
        public WeekBL(WeekBL week)
        {
            this.Breakfasts = week.Breakfasts;
            this.Dinners = week.Dinners;
            this.Lunches = week.Lunches;
            this.LunchesDB = week.LunchesDB;
            this.FirstDay = week.FirstDay;
            this.LastDay = week.LastDay;
        }

    }
    public class MealFillingBL : INotifyPropertyChanged
    {
        public MealFiillingModel MealFilling { get; set; }
        public Food Main { get { return _main; } set { _main = value; OnPropertyChanged("Main"); } }
        private Food _main = null!;
        public Food Garnish { get { return _garnish; } set { _garnish = value; OnPropertyChanged("Garnish"); } }
        private Food _garnish = null!;
        public Food SaladOrSoup { get { return _saladOrSoup; } set { _saladOrSoup = value; OnPropertyChanged("SaladOrSoup"); } }
        private Food _saladOrSoup = null!;
        public int Week_Id { get; set; }
        public string Name { get { return _name; } set { _name = value ?? ""; OnPropertyChanged("Name"); } }
        private string _name = "";
        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public MealFillingBL(Food main, Food garnish, Food saladOrSoup, int week_Id, string type)
        {
            Main = main;
            Garnish = garnish;
            SaladOrSoup = saladOrSoup;
            Name = $"{main.Name} + {garnish.Name} + {saladOrSoup.Name}";
            MealFilling = new MealFiillingModel(this);
        }
        public MealFillingBL() { }
    }
    public class MealFillingContext : DbContext
    {
        public DbSet<MealFiillingModel> mealFillings { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=WeekMealFillingDataBase.db");
        }
    }
    public class BreakfastsContext : DbContext
    {
        public DbSet<Food> Breakfasts { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Breakfasts.db");
        }
    }
    public class MealFiillingModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int MainFood_Id { get; set; }
        public int GarnishFood_Id { get; set; }
        public int SaladOrSoup_Id { get; set; }
        public int Week_Id { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public MealFiillingModel()
        { }
        public MealFiillingModel(MealFillingBL meal)
        {
            MainFood_Id = meal.Main.Id;
            GarnishFood_Id = meal.Garnish.Id;
            SaladOrSoup_Id = meal.SaladOrSoup.Id;
            Week_Id = meal.Week_Id;
        }
    }
}

