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
    public class SettingsViewModel:ViewModelBase
    {
        public ICommand NavigateToAddFood { get; set; }
        public ICommand NavigateToCalculator { get; set; }
        public ICommand AddNewType { get; set; }
        public ICommand RemoveType { get; set; }
        public NavigationStore NavigationStore { get; set; }
        public DataStore DataStore { get; set; }
        public ObservableCollection<string> FoodTypes { get; set; }
        public ObservableCollection<int> MaxMealFillingFoodQuantity { get; set; } = new ObservableCollection<int>() { 1, 2, 3, 4, 5, 6 };

        public string? Type { get { return type; } set { if (value != null) { type = value; OnPropertyChanged(nameof(Type)); } } }
        private string? type;


        public int BreakfastFoodQuantity { get { return BreakfastFoodTypeList.Count; } set { if (value > 0 && value <= 6) { ChangeFoodquant(value, BreakfastFoodTypeList); OnPropertyChanged(nameof(BreakfastFoodQuantity)); } } }
        public ObservableCollection<Food> BreakfastFoodTypeList { get; set; } = new ObservableCollection<Food>() { new Food() { Id = 0 } };


        public int LunchFoodQuantity { get { return LunchFoodTypeList.Count; } set { if (value > 0 && value <= 6) { ChangeFoodquant(value, LunchFoodTypeList); OnPropertyChanged(nameof(LunchFoodQuantity)); } } }
        public ObservableCollection<Food> LunchFoodTypeList { get; set; } = new ObservableCollection<Food>() { new Food() { Id = 0 } };


        public int DinnerFoodQuantity { get { return DinnerFoodTypeList.Count; } set { if (value > 0 && value <= 6) { ChangeFoodquant(value, DinnerFoodTypeList); OnPropertyChanged(nameof(DinnerFoodQuantity)); } } }
        public ObservableCollection<Food> DinnerFoodTypeList { get; set; } = new ObservableCollection<Food>() { new Food() { Id = 0 } };
        public SettingsViewModel(NavigationStore navigationStore, DataStore dataStore)
        {
            NavigationStore = navigationStore;
            DataStore = dataStore;
            NavigateToCalculator = new NavigateCommand<CalculatorViewModel>(NavigationStore, () => new CalculatorViewModel(NavigationStore, DataStore));
            NavigateToAddFood = new NavigateCommand<AddFoodViewModel>(NavigationStore, () => new AddFoodViewModel(NavigationStore, DataStore));
            FoodTypes = DataStore.GetFoodTypes();

            
            UpdateFoodTypes();

            AddNewType = new ButtonCommand(obj =>
            {
                if (Type == "" || Type == null)
                    return;
                bool Exist = false;
                foreach (string f in FoodTypes)
                    if (f == Type)
                    { Exist = true; }
                if (!Exist)
                    FoodTypes.Add(Type);
                Type = "";

            });
            RemoveType = new ButtonCommand(obj =>
            {                
                FoodTypes.Remove((string)obj);
            });
        }
        void UpdateFoodTypes()
        {
            foreach (Food f in BreakfastFoodTypeList)
            {
                f.FoodTypes = FoodTypes;
            }
            foreach (Food f in LunchFoodTypeList)
            {
                f.FoodTypes = FoodTypes;
            }
            foreach (Food f in DinnerFoodTypeList)
            {
                f.FoodTypes = FoodTypes;
            }

        }
        public void ChangeFoodquant(int quant, ObservableCollection<Food> FL)
        {
            while (FL.Count != quant)
            {
                if (FL.Count > quant)
                {
                    FL.RemoveAt(FL.Count - 1);
                }
                else
                {
                    FL.Add(new Food());
                    FL.Last().Id = FL[^2].Id + 1;
                }
            }
            UpdateFoodTypes();
        }
    }
}
