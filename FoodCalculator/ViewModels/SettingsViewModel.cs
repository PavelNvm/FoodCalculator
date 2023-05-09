using FoodCalculator.Commands;
using FoodCalculator.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodCalculator.ViewModels
{
    public class SettingsViewModel:ViewModelBase
    {
        public SettingsViewModel(NavigationStore navigationStore, DataStore dataStore)
        {
            NavigationStore = navigationStore;
            DataStore = dataStore;
            NavigateToCalculator = new NavigateCommand<CalculatorViewModel>(NavigationStore, () => new CalculatorViewModel(NavigationStore, DataStore));
        }
        public ICommand NavigateToCalculator { get; set; }
        public NavigationStore NavigationStore { get; set; }
        public DataStore DataStore { get; set; }
        ~SettingsViewModel() { }
    }
}
