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
    public class AddFoodViewModel:ViewModelBase
    {
        public NavigationStore NavigationStore { get; set; }
        public DataStore DataStore { get; set; }
        public ICommand NavigateToSettings { get; set; }
        public ICommand NavigateToCalculator { get; set; }
        public AddFoodViewModel(NavigationStore navigationStore, DataStore dataStore)
        {
            NavigationStore = navigationStore;
            DataStore = dataStore;
            NavigateToSettings = new NavigateCommand<SettingsViewModel>(NavigationStore, () => new SettingsViewModel(NavigationStore, DataStore));
            NavigateToCalculator = new NavigateCommand<CalculatorViewModel>(NavigationStore, () => new CalculatorViewModel(NavigationStore, DataStore));
        }
        public ObservableCollection<Food> FoodList { get { return _foodList; } set { _foodList = value; OnPropertyChanged("FoodList"); } }
        private ObservableCollection<Food> _foodList = new ObservableCollection<Food>();
    }
}
