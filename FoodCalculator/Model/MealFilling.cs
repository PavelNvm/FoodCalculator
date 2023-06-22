using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    public class MealFilling : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        //public string? FoodListOrder { get; set; } = "";
        //public int FoodQuantity { get { return FoodTypeList.Count(); } set { if (value > 0 && value <= 6) { ChangeFoodquant(value); OnPropertyChanged("FoodQuantity"); } } }
        //public ObservableCollection<Food> FoodTypeList { get; set; } = new();
        public List<Food> FoodList { get { return _foodList; } 
            set { _foodList = value;ChangeOutputValue(); } }
        private List<Food> _foodList = new();
        public string OutputValue { get { return _outputValue; }set { _outputValue = value;OnPropertyChanged(nameof(OutputValue)); } }
        private string _outputValue;
        public int Day_Id { get; set; }
        public MealFilling(string type) 
        {
            Type=type;
            ChangeOutputValue();
        }
        private void ChangeOutputValue()
        {
            if (FoodList.Count == 0)
            {
                OutputValue = "asd";
                return;
            }
            StringBuilder res = new StringBuilder();
            foreach (Food food in FoodList)
            {
                res.Append(food.ToString());
                res.Append(" + ");
            }
            res.Remove(res.Length - 3, 3);
            OutputValue = res.ToString();
        }
        public void AddFood(Food food)

        {
            FoodList.Add(food);
            //FoodListOrder += food.Id;
            //FoodListOrder += " ";
        }
        //public void ChangeFoodquant(int quant)
        //{
        //    while (FoodTypeList.Count != quant)
        //    {
        //        if (FoodTypeList.Count > quant)
        //        {
        //            FoodTypeList.RemoveAt(FoodTypeList.Count - 1);
        //        }
        //        else
        //        {
        //            FoodTypeList.Add(new Food());
        //        }
        //    }
        //}
        //public void Normalize()
        //{
        //    List<int> Ids = new List<int>();
        //    foreach (string a in FoodListOrder.Split(' ').ToList())
        //        if (a != "")
        //            Ids.Add(int.Parse(a));
        //    if (FoodList.Count != Ids.Count)
        //    {
        //        List<Food> temp = new List<Food>();
        //        foreach (int i in Ids)
        //        {
        //            var s = from f in FoodList where f.Id == i select f;
        //            temp.Add(s.First());
        //        }
        //        FoodList = temp;
        //    }
        //}
        //public override string ToString()
        //{
        //    if (FoodList.Count == 0)
        //        return new String("asd");
        //    StringBuilder res = new StringBuilder();
        //    foreach (Food food in FoodList)
        //    {
        //        res.Append(food.ToString());
        //        res.Append(" + ");
        //    }
        //    res.Remove(res.Length-3, 3);
        //    return res.ToString();
        //}
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        internal void ClearAllFood()
        {
            FoodList.Clear();
        }
    }
}
