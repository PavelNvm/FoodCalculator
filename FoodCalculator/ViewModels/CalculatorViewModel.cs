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
        public ICommand CancelCalculationCommand { get; set; }
        public ObservableCollection<bool> weeks { get; set; } = new ObservableCollection<bool> { true, false }; 


        //
        //MealFillings is list of strings for displaying in view.
        //index 0-2 is for day 1
        //index 3-5 is for day 2 etc
        public ObservableCollection<string> MealFillings { get; set; } = new ObservableCollection<string>();
        public Week WeekForDisplaying { get; set; } 
        private Week CurrentWeek { get; set; } 
        private Week NextWeek { get; set; }
        public CalculatorViewModel(NavigationStore navigationStore,DataStore dataStore) 
        {
            DataStore = dataStore;
            WeekForDisplaying = new Week(Algorithms.WeekBorders(0));
            CurrentWeek = dataStore.CurrentWeek;
            NextWeek = dataStore.NextWeek;
            WeekForDisplaying.equate(CurrentWeek);
            NavigationStore = navigationStore;
            NavigateToSettings = new NavigateCommand<SettingsViewModel>(NavigationStore, () => new SettingsViewModel(NavigationStore, DataStore));
            NavigateToAddFood = new NavigateCommand<AddFoodViewModel>(NavigationStore,()=> new AddFoodViewModel(NavigationStore,DataStore));
            Calculator = new Calculator(WeekForDisplaying, DataStore.GetFoodList(), DataStore.DayTemplates, dataStore.GetFoodTypes());
            CalculateFoodCommand = new ButtonCommand(obj => 
            {
                Calculator.Calculate();                
                
            });
            ShowNextWeekCommand = new ButtonCommand(obj =>
            {
                if (weeks[1] == false)
                {
                    weeks[0] = false; weeks[1] = true;
                    CurrentWeek.equate(WeekForDisplaying);
                    WeekForDisplaying.equate(NextWeek);
                    Calculator = new Calculator(WeekForDisplaying, DataStore.GetFoodList(), DataStore.DayTemplates, dataStore.GetFoodTypes());
                }

            });
            ShowCurrentWeekCommand = new ButtonCommand(obj => 
            {
                if (weeks[0] == false)
                {
                    weeks[0] = true; weeks[1] = false;
                    NextWeek.equate(WeekForDisplaying);
                    WeekForDisplaying.equate(CurrentWeek);
                    Calculator = new Calculator(WeekForDisplaying, DataStore.GetFoodList(), DataStore.DayTemplates, dataStore.GetFoodTypes());
                }
            });
            CancelCalculationCommand = new ButtonCommand(obj =>
            {
                
            });
            SaveWeekCommand = new ButtonCommand(obj =>
            {
                DataStore.InsertWeek(WeekForDisplaying);
            });
        }
    }
}
