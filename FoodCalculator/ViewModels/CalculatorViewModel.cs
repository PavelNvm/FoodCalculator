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
        public ObservableCollection<string> FoodTypes { get; set; }
        public NavigationStore NavigationStore { get; set; }
        public DataStore DataStore { get; set; }
        public ICommand NavigateToSettings { get; set; }
        public ICommand NavigateToAddFood { get; set; }
        
        public CalculatorViewModel(NavigationStore navigationStore,DataStore dataStore) 
        {
            NavigationStore = navigationStore;
            DataStore = dataStore;            
            NavigateToSettings = new NavigateCommand<SettingsViewModel>(NavigationStore, () =>new SettingsViewModel(NavigationStore, DataStore));
            NavigateToAddFood = new NavigateCommand<AddFoodViewModel>(NavigationStore,()=> new AddFoodViewModel(NavigationStore,DataStore));
            FoodList = DataStore.FoodList;
            FoodTypes = DataStore.FoodTypes;
        }
        public ObservableCollection<Food> FoodList { get { return _foodList; } set { _foodList = value; OnPropertyChanged("FoodList"); } }
        private ObservableCollection<Food> _foodList = new ObservableCollection<Food>();
    }
}
