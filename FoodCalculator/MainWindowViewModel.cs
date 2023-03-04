using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodCalculator
{
    class DayOfFood : INotifyPropertyChanged
    {
        //public string Name; //наверное пригодится
        public Food Breakfast { get { return _breakfast; } set { _breakfast = value; OnPropertyChanged("Breakfast"); } }
        private Food _breakfast = null!;
        public Food Lunch { get { return _lunch;  } set { _lunch = value; OnPropertyChanged("Lunch"); LunchWithSalad = value.Name + " + " + LunchSalad; } }
        private Food _lunch = null!;
        public Food LunchSalad { get { return _lunchSalad; } set { _lunchSalad = value; OnPropertyChanged("LunchSalad"); LunchWithSalad = Lunch + " + " + value.Name; } }
        private Food _lunchSalad = null!;
        public Food Dinner { get { return _dinner; } set { _dinner = value; OnPropertyChanged("Dinner"); DinnerWithSalad = value.Name + " + " + DinnerSalad; } }
        private Food _dinner = null!;
        public Food DinnerSalad { get { return _dinnerSalad; } set { _dinnerSalad = value; OnPropertyChanged("DinnerSalad"); DinnerWithSalad = Dinner + " + " + value.Name; } }
        private Food _dinnerSalad = null!;
        public string LunchWithSalad { get { return _lunchWithSalad; } set { _lunchWithSalad = value ?? ""; OnPropertyChanged("LunchWithSalad"); } }
        private string _lunchWithSalad = "";
        public string DinnerWithSalad { get { return _dinnerWithSalad; } set { _dinnerWithSalad = value ?? ""; OnPropertyChanged("DinnerWithSalad"); } }
        private string _dinnerWithSalad = "";
        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public DayOfFood(Food breakfast, Food lunch, Food lunchSalad, Food dinner, Food dinnerSalad)
        {
            Breakfast = breakfast;
            Lunch = lunch;
            LunchSalad = lunchSalad;
            Dinner = dinner;
            DinnerSalad = dinnerSalad;
            LunchWithSalad = Lunch + " + " + LunchSalad;
            DinnerWithSalad = Dinner + " + " + DinnerSalad;
        }
        public DayOfFood()
        {

        }
    }
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public AddFoodWindow AddFoodWindow { get; set; } = new AddFoodWindow();
        public static Random random = new Random();
        public ObservableCollection<Food> FoodList { get { return _foodList; } set { _foodList = value; OnPropertyChanged("FoodList"); } }
        private ObservableCollection<Food> _foodList = new ObservableCollection<Food>();

        public ObservableCollection<string> BreakfastRationList { get { return _breakfastRationList; } set { if (value != null) { _breakfastRationList = value; OnPropertyChanged("BreakfastRationList"); } } }
        private ObservableCollection<string> _breakfastRationList = null!;
        public List<DayOfFood> WeekOfFood { get; set; } = new List<DayOfFood> { new DayOfFood(),new DayOfFood(), new DayOfFood(), new DayOfFood(), new DayOfFood(), new DayOfFood(), new DayOfFood() };
        public ObservableCollection<Food> BreakfastFood { get; set; }
        public ObservableCollection<string> DinnerRationList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> LunchRationList { get; set; } = new ObservableCollection<string>();
        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public RelayCommand CalcFoodCommand { get; set; }
        public RelayCommand OpenAddFoodWindowCommand { get; set; }
        public MainWindowViewModel()
        {
            CalcFoodCommand = new RelayCommand(obj =>
            {
                if (Linker.ViewModels.Count > 0)
                {

                    Week CurrentWeek = new Week();
                    WeekOfFood = new List<(Food BreakFast, Food Lunch, Food LunchSalad, Food Dinner, Food DinnerSalad)>();
                    FoodCalcer foodCalculator = (FoodCalcer)Linker.ViewModels.First();
                    FoodList = foodCalculator.FoodList;
                    List<Food> breakfastFood = new List<Food>();
                    List<Food> garnishFood = new List<Food>();
                    List<Food> mainFood = new List<Food>();
                    List<Food> soupFood = new List<Food>();
                    List<Food> saladFood = new List<Food>();
                    List<Food> breakfastFoodQueue = new List<Food>();
                    List<Food> garnishFoodQueue = new List<Food>();
                    List<Food> mainFoodQueue = new List<Food>();
                    List<Food> soupFoodQueue = new List<Food>();
                    List<Food> saladFoodQueue = new List<Food>();

                    List<Food> BreaksfastWeek = new List<Food>();
                    List<Food> LunchtWeek = new List<Food>();
                    List<Food> DinnerWeek = new List<Food>();


                    foreach (var element in FoodList)
                    {
                        if (element.Type == Food.FoodType.Eggs.ToString() || element.Type == Food.FoodType.KaWa.ToString())
                        {
                            for (int i = 0; i < element.Modifier; i++)
                                breakfastFood.Add(element);
                        }
                        else if (element.Type == Food.FoodType.Garnish.ToString())
                        {
                            for (int i = 0; i < element.Modifier; i++)
                                garnishFood.Add(element);
                        }
                        else if (element.Type == Food.FoodType.Main.ToString())
                        {
                            for (int i = 0; i < element.Modifier; i++)
                                mainFood.Add(element);
                        }
                        else if (element.Type == Food.FoodType.Soup.ToString())
                        {
                            for (int i = 0; i < element.Modifier; i++)
                                soupFood.Add(element);
                        }
                        else if (element.Type == Food.FoodType.Salad.ToString())
                        {
                            for (int i = 0; i < element.Modifier; i++)
                                saladFood.Add(element);
                        }
                    }

                    {
                        for (int i = 0; i < 7; i++)
                        {
                            WeekOfFood.Add(new(ChoseFood(breakfastFood), ChoseFood(mainFood), ChoseFood(saladFood), ChoseFood(mainFood), ChoseFood(saladFood)));
                        }
                    }
                        Food ChoseFood(List<Food> f)
                        {
                            Random rnd = new Random();
                            return f[rnd.Next(f.Count())];
                        }
                        void FillQueue(List<Food> queue, List<Food> food)
                        {
                            Random rnd = new Random();
                            Food lastfood = food[rnd.Next(food.Count())];
                            int portions = lastfood.Portions;
                            while (queue.Count < 7)
                            {
                                while (portions > 0)
                                {
                                    queue.Add(lastfood);
                                    portions--;
                                }
                                point:
                                lastfood = food[rnd.Next(food.Count())];
                                if (lastfood.Id == queue.Last().Id&&food.Count!=1)
                                    goto point;
                                portions = lastfood.Portions;
                            }
                        }
                    }
                    catch { }
                    #region
                    try
                    {
                        MondayBreakfastRation = WeekOfFood[0].BreakFast.Name;
                        TuesdayBreakfastRation = WeekOfFood[1].BreakFast.Name;
                        WednesdayBreakfastRation = WeekOfFood[2].BreakFast.Name;
                        ThursdayBreakfastRation = WeekOfFood[3].BreakFast.Name;
                        FridayBreakfastRation = WeekOfFood[4].BreakFast.Name;
                        SaturdayBreakfastRation = WeekOfFood[5].BreakFast.Name;
                        SundayBreakfastRation = WeekOfFood[6].BreakFast.Name;
                        MondayLunchRation = WeekOfFood[0].Lunch.Name + " + " + WeekOfFood[0].LunchSalad.Name;
                        TuesdayLunchRation = WeekOfFood[1].Lunch.Name + " + " + WeekOfFood[1].LunchSalad.Name;
                        WednesdayLunchRation = WeekOfFood[2].Lunch.Name + " + " + WeekOfFood[2].LunchSalad.Name;
                        ThursdayLunchRation = WeekOfFood[3].Lunch.Name + " + " + WeekOfFood[3].LunchSalad.Name;
                        FridayLunchRation = WeekOfFood[4].Lunch.Name + " + " + WeekOfFood[4].LunchSalad.Name;
                        SaturdayLunchRation = WeekOfFood[5].Lunch.Name + " + " + WeekOfFood[5].LunchSalad.Name;
                        SundayLunchRation = WeekOfFood[6].Lunch.Name + " + " + WeekOfFood[6].LunchSalad.Name;
                        MondayDinnerRation = WeekOfFood[0].Dinner.Name + " + " + WeekOfFood[0].DinnerSalad.Name;
                        TuesdayDinnerRation = WeekOfFood[1].Dinner.Name + " + " + WeekOfFood[1].DinnerSalad.Name;
                        WednesdayDinnerRation = WeekOfFood[2].Dinner.Name + " + " + WeekOfFood[2].DinnerSalad.Name;
                        ThursdayDinnerRation = WeekOfFood[3].Dinner.Name + " + " + WeekOfFood[3].DinnerSalad.Name;
                        FridayDinnerRation = WeekOfFood[4].Dinner.Name + " + " + WeekOfFood[4].DinnerSalad.Name;
                        SaturdayDinnerRation = WeekOfFood[5].Dinner.Name + " + " + WeekOfFood[5].DinnerSalad.Name;
                        SundayDinnerRation = WeekOfFood[6].Dinner.Name + " + " + WeekOfFood[6].DinnerSalad.Name;
                    }
                    catch
                    {

                    }
                    #endregion


                }
            });
             OpenAddFoodWindowCommand = new RelayCommand(obj =>
            {
                try
                {
                    AddFoodWindow.Show();
                    AddFoodWindow.Focus();
                }
                catch
                {
                    AddFoodWindow = new AddFoodWindow();
                    AddFoodWindow.Show();
                }
                finally
                {
                }
            });
        }
    }
}
