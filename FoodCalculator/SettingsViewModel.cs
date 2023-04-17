using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FoodCalculator
{
   
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<int> MaxMealFillingFoodQuantity { get; set; } = new ObservableCollection<int>() { 1, 2, 3, 4, 5, 6 };

        public string Type { get { return type; } set { if (value != null) { type = value; OnPropertyChanged("Type"); } } }
        private string type;

        public ObservableCollection<string> FoodTypes { get; set; }
        public int BreakfastFoodQuantity { get { return BreakfastFoodTypeList.Count(); } set { if (value > 0 && value <= 6) { ChangeFoodquant(value,BreakfastFoodTypeList); OnPropertyChanged("BreakfastFoodQuantity"); } } }
        public ObservableCollection<Food> BreakfastFoodTypeList { get; set; }=new ObservableCollection<Food>() { new Food() { Id = 0 }};


        public int LunchFoodQuantity { get { return LunchFoodTypeList.Count(); } set { if (value > 0 && value <= 6) { ChangeFoodquant(value, LunchFoodTypeList); OnPropertyChanged("LunchFoodQuantity"); } } }
        public ObservableCollection<Food> LunchFoodTypeList { get; set; } = new ObservableCollection<Food>() { new Food() { Id = 0 } };


        public int DinnerFoodQuantity { get { return DinnerFoodTypeList.Count(); } set { if (value > 0 && value <= 6) { ChangeFoodquant(value, DinnerFoodTypeList); OnPropertyChanged("DinnerFoodQuantity"); } } }
        public ObservableCollection<Food> DinnerFoodTypeList { get; set; } = new ObservableCollection<Food>() { new Food() { Id = 0 } };

        public ObservableCollection<string> FoodTypesSettings { get; set; } = new();

        public RelayCommand AddNewType { get; set; }
        public RelayCommand RemoveType { get; set; }



        public SettingsViewModel()
        {

            FoodTypes = new ObservableCollection<string>(Enum.GetNames(typeof(Food.FoodType)));
            FoodTypesSettings.Add("Tst");

            AddNewType = new RelayCommand(obj =>
            {
            if (Type == "" || Type == null)
                    return;
                bool Exist = false;
                foreach (string f in FoodTypesSettings)
                    if (f == Type)
                    { Exist = true; }
                    if(!Exist)
                FoodTypesSettings.Add(Type);                
                Type = "";
            });
            RemoveType = new RelayCommand(obj =>
            { 
                FoodTypesSettings.Remove(obj as string);           
            });
        }
        public void ChangeFoodquant(int quant,ObservableCollection<Food> FL)
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
                    FL.Last().Id = FL[FL.Count - 2].Id+1;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
