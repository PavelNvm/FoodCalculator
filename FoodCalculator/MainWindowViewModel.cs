using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public static Random random = new Random();
        public ObservableCollection<Food> FoodList { get { return _foodList; } set { _foodList = value; OnPropertyChanged("FoodList"); } }
        private ObservableCollection<Food> _foodList = new ObservableCollection<Food>();

        public ObservableCollection<string> BreakfastRationList { get { return _breakfastRationList; } set { if (value != null) { _breakfastRationList = value; OnPropertyChanged("BreakfastRationList"); } } }
        private ObservableCollection<string> _breakfastRationList;
        private List<(Food BreakFast, Food Lunch, Food LunchSalad, Food Dinner, Food DinnerSalad)> WeekOfFood;
        #region
        private string _mondayBreakfastRation;
        public string MondayBreakfastRation { get { return _mondayBreakfastRation; } set { if (value != null) { _mondayBreakfastRation = value; OnPropertyChanged("MondayBreakfastRation"); } } }
        public string _tuesdayBreakfastRation;
        public string TuesdayBreakfastRation { get { return _tuesdayBreakfastRation; } set { if (value != null) { _tuesdayBreakfastRation = value; OnPropertyChanged("TuesdayBreakfastRation"); } } }
        private string _wednesdayBreakfastRation;
        public string WednesdayBreakfastRation { get { return _wednesdayBreakfastRation; } set { if (value != null) { _wednesdayBreakfastRation = value; OnPropertyChanged("WednesdayBreakfastRation"); } } }
        private string _thursdayBreakfastRation;
        public string ThursdayBreakfastRation { get { return _thursdayBreakfastRation; } set { if (value != null) { _thursdayBreakfastRation = value; OnPropertyChanged("ThursdayBreakfastRation"); } } }
        private string _fridayBreakfastRation;
        public string FridayBreakfastRation { get { return _fridayBreakfastRation; } set { if (value != null) { _fridayBreakfastRation = value; OnPropertyChanged("FridayBreakfastRation"); } } }
        private string _saturdayBreakfastRation;
        public string SaturdayBreakfastRation { get { return _saturdayBreakfastRation; } set { if (value != null) { _saturdayBreakfastRation = value; OnPropertyChanged("SaturdayBreakfastRation"); } } }
        private string _sundayBreakfastRation;
        public string SundayBreakfastRation { get { return _sundayBreakfastRation; } set { if (value != null) { _sundayBreakfastRation = value; OnPropertyChanged("SundayBreakfastRation"); } } }
        private string _mondayLunchRation;
        public string MondayLunchRation { get { return _mondayLunchRation; } set { if (value != null) { _mondayLunchRation = value; OnPropertyChanged("MondayLunchRation"); } } }
        private string _tuesdayLunchRation;
        public string TuesdayLunchRation { get { return _tuesdayLunchRation; } set { if (value != null) { _tuesdayLunchRation = value; OnPropertyChanged("TuesdayLunchRation"); } } }
        private string _wednesdayLunchRation;
        public string WednesdayLunchRation { get { return _wednesdayLunchRation; } set { if (value != null) { _wednesdayLunchRation = value; OnPropertyChanged("WednesdayLunchRation"); } } }
        private string _thursdayLunchRation;
        public string ThursdayLunchRation { get { return _thursdayLunchRation; } set { if (value != null) { _thursdayLunchRation = value; OnPropertyChanged("ThursdayLunchRation"); } } }
        private string _fridayLunchRation;
        public string FridayLunchRation { get { return _fridayLunchRation; } set { if (value != null) { _fridayLunchRation = value; OnPropertyChanged("FridayLunchRation"); } } }
        private string _saturdayLunchRation;
        public string SaturdayLunchRation { get { return _saturdayLunchRation; } set { if (value != null) { _saturdayLunchRation = value; OnPropertyChanged("SaturdayLunchRation"); } } }
        private string _sundayLunchRation;
        public string SundayLunchRation { get { return _sundayLunchRation; } set { if (value != null) { _sundayLunchRation = value; OnPropertyChanged("SundayLunchRation"); } } }
        private string _mondayDinnerRation;
        public string MondayDinnerRation { get { return _mondayDinnerRation; } set { if (value != null) { _mondayDinnerRation = value; OnPropertyChanged("MondayDinnerRation"); } } }
        private string _tuesdayDinnerRation;
        public string TuesdayDinnerRation { get { return _tuesdayDinnerRation; } set { if (value != null) { _tuesdayDinnerRation = value; OnPropertyChanged("TuesdayDinnerRation"); } } }
        private string _wednesdayDinnerRation;
        public string WednesdayDinnerRation { get { return _wednesdayDinnerRation; } set { if (value != null) { _wednesdayDinnerRation = value; OnPropertyChanged("WednesdayDinnerRation"); } } }
        private string _thursdayDinnerRation;
        public string ThursdayDinnerRation { get { return _thursdayDinnerRation; } set { if (value != null) { _thursdayDinnerRation = value; OnPropertyChanged("ThursdayDinnerRation"); } } }
        private string _fridayDinnerRation;
        public string FridayDinnerRation { get { return _fridayDinnerRation; } set { if (value != null) { _fridayDinnerRation = value; OnPropertyChanged("FridayDinnerRation"); } } }
        private string _saturdayDinnerRation;
        public string SaturdayDinnerRation { get { return _saturdayDinnerRation; } set { if (value != null) { _saturdayDinnerRation = value; OnPropertyChanged("SaturdayDinnerRation"); } } }
        private string _sundayDinnerRation;
        public string SundayDinnerRation { get { return _sundayDinnerRation; } set { if (value != null) { _sundayDinnerRation = value; OnPropertyChanged("SundayDinnerRation"); } } }
        #endregion

        public ObservableCollection<string> DinnerRationList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> LunchRationList { get; set; } = new ObservableCollection<string>();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public RelayCommand CalcFoodCommand { get; set; }
        public MainWindowViewModel()
        {
           
            CalcFoodCommand = new RelayCommand(obj =>
            {
                if (Linker.ViewModels.Count > 0 )
                {
                    WeekOfFood = new List<(Food BreakFast, Food Lunch, Food LunchSalad, Food Dinner, Food DinnerSalad)>();
                    FoodCalcer foodCalculator = Linker.ViewModels.First() as FoodCalcer;
                    FoodList = foodCalculator.FoodList;
                    List<Food> breakfastFood = new List<Food>();
                    List<Food> garnishFood = new List<Food>();
                    List<Food> mainFood = new List<Food>();
                    List<Food> soupFood = new List<Food>();
                    List<Food> saladFood = new List<Food>();

                    Queue<Food> breakfastFoodQueue = new Queue<Food>();
                    Queue<Food> garnishFoodQueue = new Queue<Food>();
                    Queue<Food> mainFoodQueue = new Queue<Food>();
                    Queue<Food> soupFoodQueue = new Queue<Food>();
                    Queue<Food> saladFoodQueue = new Queue<Food>();

                    List<Food> BreaksfastWeek = new List<Food>();
                    List<Food> LunchtWeek = new List<Food>();
                    List<Food> DinnerWeek = new List<Food>();

                    if (breakfastFoodQueue.Count<1)
                    {

                    }
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
