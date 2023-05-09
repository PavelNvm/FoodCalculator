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
    public class CalculatorViewModel:ViewModelBase
    {
        public NavigationStore NavigationStore { get; set; }
        public DataStore DataStore { get; set; }
        public ICommand NavigateToSettings { get; set; }
        
        public CalculatorViewModel(NavigationStore navigationStore,DataStore dataStore) 
        {
            NavigationStore = navigationStore;
            DataStore = dataStore;
            DataStore.VMAmount++;
            NavigateToSettings = new NavigateCommand<SettingsViewModel>(NavigationStore, () =>new SettingsViewModel(NavigationStore, DataStore));
        }
        ~CalculatorViewModel() { }
    }
}
