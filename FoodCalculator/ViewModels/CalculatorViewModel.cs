using FoodCalculator.Commands;
using FoodCalculator.Model;
using FoodCalculator.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodCalculator.ViewModels
{
    public class CalculatorViewModel:ViewModelBase
    {        
        public NavigationStore NavigationStore { get; set; }
        public DataStore DataStore { get; set; }
        private Calculator Calculator;
        public ICommand NavigateToSettings { get; set; }
        public ICommand NavigateToAddFood { get; set; }
        public ICommand CalculateFoodCommand { get; set; }
        public ICommand SaveWeekCommand { get; set; }
        public ICommand ShowCurrentWeekCommand { get; set; }
        public ICommand ShowNextWeekCommand { get; set; }


        //
        //MealFillings is list of strings for displaying in view.
        //index 0-2 is for day 1
        //index 3-5 is for day 2 etc
        public ObservableCollection<string> MealFillings { get; set; } = new ObservableCollection<string>();
        public Week WeekForDisplaying { get; set; } 
        private Week CurrentWeek { get; set; } = new Week(); 
        private Week NextWeek { get; set; } = new Week();
        public CalculatorViewModel(NavigationStore navigationStore,DataStore dataStore) 
        {
            WeekForDisplaying = new Week();
            CurrentWeek = new Week(curwek(0));
            NextWeek = new Week(curwek(1));
            WeekForDisplaying.equate(CurrentWeek);
            NavigationStore = navigationStore;
            DataStore = dataStore;            
            NavigateToSettings = new NavigateCommand<SettingsViewModel>(NavigationStore, () =>new SettingsViewModel(NavigationStore, DataStore));
            NavigateToAddFood = new NavigateCommand<AddFoodViewModel>(NavigationStore,()=> new AddFoodViewModel(NavigationStore,DataStore));
            Calculator = new Calculator(NextWeek, DataStore.GetFoodList(), DataStore.DayTemplates, dataStore.GetFoodTypes());
            CalculateFoodCommand = new ButtonCommand(obj => 
            {
                Calculator.Calculate();                
                
            });
            ShowNextWeekCommand = new ButtonCommand(obj =>
            {
                WeekForDisplaying.equate(NextWeek);
                Calculator = new Calculator(WeekForDisplaying, DataStore.GetFoodList(), DataStore.DayTemplates, dataStore.GetFoodTypes());

            });
            ShowCurrentWeekCommand = new ButtonCommand(obj => 
            {
                WeekForDisplaying.equate(CurrentWeek);
                Calculator = new Calculator(WeekForDisplaying, DataStore.GetFoodList(), DataStore.DayTemplates, dataStore.GetFoodTypes());
            });
        }
        
        
        (DateOnly, DateOnly) curwek(int offset)//offset in weeks
        {
            int day = Convert.ToInt32(DateTime.Now.DayOfWeek);
            DateOnly fd = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            fd = fd.AddDays(-day + 1 + 7 * offset);
            DateOnly sd = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            sd = sd.AddDays(7 - day + 7 * offset);


            return (fd, sd);
        }
    }
}
