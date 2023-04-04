using Microsoft.EntityFrameworkCore;
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
   
    public class SettingsViewModel :INotifyPropertyChanged
    {
        public ObservableCollection<int> CBE { get; set; } =new ObservableCollection<int>() { 1,2,3,4,5,6};
        public ObservableCollection<string> FoodTypes { get; set; }
        public MealFilling BreakfastSetting { get; set; } = new MealFilling() { FoodQuantity=3};
        public MealFilling LunchSetting { get; set; } = new MealFilling();
        public MealFilling DinnerSetting { get; set; } = new MealFilling();
        public RelayCommand SaveChangesCommand { get; set; }
        //public int 
        public ObservableCollection<Food> BreakFastFoodTypeList { get; set; }



        public SettingsViewModel()
        {
            
            FoodTypes = new ObservableCollection<string>(Enum.GetNames(typeof(Food.FoodType)));
            SaveChangesCommand = new RelayCommand(obj =>
            {
                if(BreakfastSetting.FoodTypeList.Count==3)
                    BreakfastSetting.FoodTypeList[2].Portions=3;
                object[] objects = obj as object[];
                if (1 == 1) { }
            });

        }
        
        public event PropertyChangedEventHandler? PropertyChanged = null!;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
