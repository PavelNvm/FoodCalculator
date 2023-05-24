using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Stores
{
    public class DataStore
    {
        private ObservableCollection<Food> FoodList { get; set; }
        private ObservableCollection<string> FoodTypes { get; set; }
        public DataStore() 
        {
            GetFoodListFromDB();
            GetFoodTypesFromDB();
        }
        public ObservableCollection<string> GetFoodTypes()
        {
            return FoodTypes;
        }
        private void GetFoodListFromDB()
        {

        }
        private void GetFoodTypesFromDB()
        {
            FoodTypes = new ObservableCollection<string>() {"raz","dva" };
        }
    }
}
