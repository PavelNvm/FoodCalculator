using FoodCalculator.Commands;
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
        public CalculatorViewModel(NavigationStore navigationStore,DataStore dataStore) 
        {
            
            NavigationStore = navigationStore;
            DataStore = dataStore;            
            NavigateToSettings = new NavigateCommand<SettingsViewModel>(NavigationStore, () =>new SettingsViewModel(NavigationStore, DataStore));
            NavigateToAddFood = new NavigateCommand<AddFoodViewModel>(NavigationStore,()=> new AddFoodViewModel(NavigationStore,DataStore));
            CalculateFoodCommand = new ButtonCommand(obj => 
            {
                Random rnd = new Random();
                testik(rnd.Next(100));                
                
            });
        }
        
        private async void  testik (int mn)
        {
            await Task.Run(() => 
            {
                MealFillings.Clear();
                for (int i = mn; i < mn + 21; i++)
                    MealFillings.Add(i.ToString());
            });
            
        }
    }
}
