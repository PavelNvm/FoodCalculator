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
    public class SettingsViewModel : ViewModelBase
    {
        public ICommand NavigateToCalculator { get; set; }
        public ICommand SaveAndNavigateCommand { get; set; }
        public ICommand AddNewType { get; set; }
        public ICommand RemoveType { get; set; }
        public ICommand ChooseDayCommand { get; set; }
        public ICommand ApplyToEachDayCommand { get; set; }
        public ICommand CopyDayCommand { get; set; }
        public ICommand PasteDayCommand { get; set; }
        public ICommand RandomCommand { get; set; }
        public NavigationStore NavigationStore { get; set; }
        public DataStore DataStore { get; set; }
        public ObservableCollection<string> FoodTypes { get; set; }
        public ObservableCollection<int> MaxMealFillingFoodQuantity { get; set; } = new ObservableCollection<int>() { 1, 2, 3, 4, 5, 6 };
        public ObservableCollection<DayTemplate> DayTemplates { get; set; }
        public DayTemplate DayForDisplaying { get; set; } = new();
        private DayTemplate _clipboard = new();
        private bool _isclipboard = false;
        public ObservableCollection<bool> IsDaySelected { get; set; } = new ObservableCollection<bool>() { true, false, false, false, false, false, false };
        public int SelectedDayIndex { get; set; }
        public int BreakfastFoodQuantity { get { return DayForDisplaying.Breakfast.Count(); } set { if (value > 0 && value <= 6) { ChangeFoodquant(value, DayForDisplaying.Breakfast); OnPropertyChanged(nameof(BreakfastFoodQuantity));DataStore.UpdateSettingsInDB();  } } }
        public int LunchFoodQuantity { get { return DayForDisplaying.Lunch.Count(); } set { if (value > 0 && value <= 6) { ChangeFoodquant(value, DayForDisplaying.Lunch);             OnPropertyChanged(nameof(LunchFoodQuantity));    DataStore.UpdateSettingsInDB();  } } }
        public int DinnerFoodQuantity { get { return DayForDisplaying.Dinner.Count(); } set { if (value > 0 && value <= 6) { ChangeFoodquant(value, DayForDisplaying.Dinner);          OnPropertyChanged(nameof(DinnerFoodQuantity));   DataStore.UpdateSettingsInDB();  } } }
        public string NewFoodType { get { return _newFoodType; } set { _newFoodType = value; OnPropertyChanged(nameof(NewFoodType)); } }
        private string _newFoodType;       

        public SettingsViewModel(NavigationStore navigationStore, DataStore dataStore)
        {
            NavigationStore = navigationStore;
            DataStore = dataStore;
            NavigateToCalculator = new NavigateCommand<CalculatorViewModel>(NavigationStore, () => new CalculatorViewModel(NavigationStore, DataStore));
            FoodTypes = DataStore.GetFoodTypes();
            DayTemplates = DataStore.DayTemplates;
            SelectedDayIndex = 0;            
            DayForDisplaying.equate(DayTemplates[0]);
            UpdateQuantity();

            SaveAndNavigateCommand = new ButtonCommand(obj =>
            {
                DayTemplates[SelectedDayIndex].equate(DayForDisplaying);
                NavigateToCalculator.Execute(null);
            });


            ChooseDayCommand = new ButtonCommand(obj => 
            {
                DayTemplates[SelectedDayIndex].equate(DayForDisplaying);
                IsDaySelected[SelectedDayIndex] = false;
                SelectedDayIndex = int.Parse((string)obj);
                IsDaySelected[SelectedDayIndex] = true;
                DayForDisplaying.equate(DayTemplates[SelectedDayIndex]);
                UpdateQuantity();
            });
            RandomCommand = new ButtonCommand(obj =>
            {
                for (int i = 0; i < 7; i++)
                    DayTemplates[i].Random(FoodTypes);
                DayForDisplaying.equate(DayTemplates[SelectedDayIndex]);
                UpdateQuantity();
            });
            PasteDayCommand = new ButtonCommand(obj =>
            {
                if (_isclipboard)
                {
                    DayForDisplaying.equate(_clipboard);
                }
                UpdateQuantity();
            });
            CopyDayCommand = new ButtonCommand(obj =>
            {
                _clipboard.equate(DayForDisplaying);
                _isclipboard = true;
            });
            ApplyToEachDayCommand = new ButtonCommand(obj =>
            {
                for(int i = 0;i<7;i++)
                {
                    DayTemplates[i].equate(DayForDisplaying);
                }
            });
            AddNewType = new ButtonCommand(obj =>
            {
                string? input = obj as string;
            if (input != null&&input!="")
                {
                    if(!FoodTypes.Contains(input))
                    {
                        DataStore.AddType(input);
                    }
                }
                NewFoodType = "";
            });
            RemoveType = new ButtonCommand(obj =>
            {                
                FoodTypes.Remove((string)obj);
            });
        }       
        public void ChangeFoodquant(int quant, ObservableCollection<StringWrapper> FL)
        {
            while (FL.Count != quant)
            {
                if (FL.Count > quant)
                {
                    FL.RemoveAt(FL.Count - 1);
                }
                else
                {
                    FL.Add(new StringWrapper(FoodTypes[0]));
                }
            }
        }
        
        private void UpdateQuantity()
        {
            BreakfastFoodQuantity = DayForDisplaying.Breakfast.Count();
            LunchFoodQuantity = DayForDisplaying.Lunch.Count();
            DinnerFoodQuantity = DayForDisplaying.Dinner.Count();            
        }
    }
}
