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
        public ICommand NavigateToCalculator { get; set; }
        public ICommand DeleteCommand { get; set; }
        public AddFoodViewModel(NavigationStore navigationStore, DataStore dataStore)
        {
            NavigationStore = navigationStore;
            DataStore = dataStore;            
            NavigateToCalculator = new NavigateCommand<CalculatorViewModel>(NavigationStore, () => new CalculatorViewModel(NavigationStore, DataStore));
            DeleteCommand = new ButtonCommand(obj => 
            {
                
                FoodList.Remove(obj as Food);
            });
            popa();
        }
        public ObservableCollection<Food> FoodList { get { return _foodList; } set { _foodList = value; OnPropertyChanged(nameof(FoodList)); } }
        private ObservableCollection<Food> _foodList = new ObservableCollection<Food>();
        void popa()
        {
            FoodList.Add(new Food() { Id = 0, Name = "kaWa", Type = "KaWa" });
            FoodList.Add(new Food() { Id = 1, Name = "eggs", Type = "Eggs" });
            FoodList.Add(new Food() { Id = 2, Name = "kotleta", Type = "Main", Portions = 3 });
            FoodList.Add(new Food() { Id = 3, Name = "seledka pod shuboi", Type= "Salad" });
            FoodList.Add(new Food() { Id = 4, Name = "jarennaya chicken", Type = "Main", Portions = 4 });
            FoodList.Add(new Food() { Id = 5, Name = "Winters salad", Type = "Salad" });
            FoodList.Add(new Food() { Id = 6, Name = "rice", Type = "Garnish", Portions = 2 });
            FoodList.Add(new Food() { Id = 7, Name = "kartoxa", Type = "Garnish", Portions = 2 });
            FoodList.Add(new Food() { Id = 8, Name = "borsh", Type = "Soup", Portions = 3 });
            FoodList.Add(new Food() { Id = 9, Name = "1", Type = "KaWa" });
            FoodList.Add(new Food() { Id = 10, Name = "2", Type = "Eggs" });
            FoodList.Add(new Food() { Id = 11, Name = "k", Type = "Main", Portions = 3 });
            FoodList.Add(new Food() { Id = 12, Name = "eledka pod shuboi", Type = "Salad" });
            FoodList.Add(new Food() { Id = 13, Name = "arennaya chicken", Type = "Main", Portions = 4 });
            FoodList.Add(new Food() { Id = 14, Name = "inters salad", Type = "Salad" });
            FoodList.Add(new Food() { Id = 15, Name = "ice", Type = "Garnish", Portions = 2 });
            FoodList.Add(new Food() { Id = 16, Name = "artoxa", Type = "Garnish", Portions = 2 });
            FoodList.Add(new Food() { Id = 17, Name = "orsh", Type = "Soup", Portions = 3 });
        }
    }
}
