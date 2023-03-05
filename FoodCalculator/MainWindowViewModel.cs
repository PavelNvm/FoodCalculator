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
        //public string Name; //наверное пригодится// не пригодилось
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
        //public ObservableCollection<Week> WeekList { get; set; } = new ObservableCollection<Week>();
        public Week CurrentWeek { get; set; } = new Week();
        public Week NextWeek { get; set; } = new Week();
        public Week ShownigWeek { get; set; } = new Week();
        public ObservableCollection<Food> BreakfastFood { get; set; }
        public ObservableCollection<string> DinnerRationList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> LunchRationList { get; set; } = new ObservableCollection<string>();
        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public RelayCommand CalculateFoodCommand { get; set; }
        public RelayCommand SaveWeekCommand { get; set; }
        public RelayCommand ShowCurrentWeekCommand { get; set; }
        public RelayCommand ShowNextWeekCommand { get; set; }
        public RelayCommand OpenAddFoodWindowCommand { get; set; }
        public MainWindowViewModel()
        {
            //WeekContext WeekDB = new WeekContext();

            //if (WeekDB.WeekList.Local.ToObservableCollection().Count!=0)
            //{
            //    CurrentWeek = WeekDB.WeekList.Local.ToObservableCollection().Last();
            //    ShownigWeek = CurrentWeek;
            //}
            ShownigWeek = NextWeek;
            SaveWeekCommand = new RelayCommand(obj =>
            {

            });
            ShowNextWeekCommand = new RelayCommand(obj =>
            {
                ShownigWeek = NextWeek;
            });
            ShowCurrentWeekCommand = new RelayCommand(obj =>
            {
                ShownigWeek = CurrentWeek;
            });
            CalculateFoodCommand = new RelayCommand(obj =>
            {
                if (Linker.ViewModels.Count > 0)
                {
                    Week CurrentWeek = new Week();                    
                    FoodCalcer foodCalculator = (FoodCalcer)Linker.ViewModels.First();
                    FoodList = foodCalculator.FoodList;
                    List<Food> breakfastFood = new List<Food>();
                    List<Food> garnishFood = new List<Food>();
                    List<Food> mainFood = new List<Food>();
                    List<Food> soupOrSaladFood = new List<Food>();                    
                    List<Food> breakfastFoodQueue = new List<Food>();
                    List<Food> garnishFoodQueue = new List<Food>();
                    List<Food> mainFoodQueue = new List<Food>();                    
                    List<Food> soupOrSaladFoodQueue = new List<Food>();

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
                                soupOrSaladFood.Add(element);
                        }
                        else if (element.Type == Food.FoodType.Salad.ToString())
                        {
                            for (int i = 0; i < element.Modifier; i++)
                                soupOrSaladFood.Add(element);
                        }
                    }


                    //for (int i = 0; i < 7; i++)
                    //{
                    //    //WeekOfFood.Add(new(ChoseFood(breakfastFood), ChoseFood(mainFood), ChoseFood(saladFood), ChoseFood(mainFood), ChoseFood(saladFood)));
                    //    WeekOfFood[i].Breakfast = ChoseFood(breakfastFood);
                    //    WeekOfFood[i].Lunch = ChoseFood(mainFood);
                    //    WeekOfFood[i].LunchSalad = ChoseFood(soupOrSaladFood);
                    //    WeekOfFood[i].Dinner = ChoseFood(mainFood);
                    //    WeekOfFood[i].DinnerSalad = ChoseFood(soupOrSaladFood);
                    //}
                    FillQueue(breakfastFoodQueue, breakfastFood,7);
                    FillQueue(mainFoodQueue, mainFood,14);
                    FillQueue(garnishFoodQueue, garnishFood,14);
                    FillQueue(soupOrSaladFoodQueue, soupOrSaladFood, 14);
                    NextWeek.FillBreakfast(breakfastFoodQueue);
                    NextWeek.FillLunchesAndDinners(mainFoodQueue, garnishFoodQueue, soupOrSaladFoodQueue);
                    Food ChoseFood(List<Food> f)
                    {
                        Random rnd = new Random();
                        return f[rnd.Next(f.Count())];
                    }
                    void FillQueue(List<Food> queue, List<Food> food,int quantity)
                    {
                        Random rnd = new Random();
                        Food lastfood = food[rnd.Next(food.Count())];
                        int portions = lastfood.Portions;
                        while (queue.Count < quantity)
                        {
                            while (portions > 0)
                            {
                                queue.Add(lastfood);
                                portions--;
                            }
                        point:
                            lastfood = food[rnd.Next(food.Count())];
                            if (lastfood.Id == queue.Last().Id && food.Count != 1)
                                goto point;
                            portions = lastfood.Portions;
                        }
                    }
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
