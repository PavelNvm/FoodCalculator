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
    public class AddFoodViewModel:ViewModelBase
    {
        public NavigationStore NavigationStore { get; set; }
        public DataStore DataStore { get; set; }        
        public ICommand NavigateToCalculator { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public AddFoodViewModel(NavigationStore navigationStore, DataStore dataStore)
        {
            NavigationStore = navigationStore;
            DataStore = dataStore;            
            NavigateToCalculator = new NavigateCommand<CalculatorViewModel>(NavigationStore, () => new CalculatorViewModel(NavigationStore, DataStore));
            DeleteCommand = new ButtonCommand(obj => 
            {                
                DataStore.RemoveFood(obj as Food);
            });
            AddCommand = new ButtonCommand(obj =>
            {
                DataStore.AddFood(obj as Food);
            });
            
        }
        public ObservableCollection<Food> FoodList => DataStore.GetFoodList();
        public ObservableCollection<string> FoodTypes=>DataStore.GetFoodTypes();
        
    }
}
