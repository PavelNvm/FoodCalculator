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
        public ObservableCollection<Food> FoodList { get; set; }
        public ObservableCollection<string> FoodTypes { get; set; }
        public DataStore() 
        {
            
        }
        public void GetFoodListFromDB()
        {

        }
        public void GetFoodTypesFromDB()
        {

        }
    }
}
