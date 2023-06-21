using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator
{
    
    //public class Food : INotifyPropertyChanged
    //{       
    //    public string Name { get; set; } = null!;        
    //    public string Type { get { return type; } set { if (value != null) { type = value; OnPropertyChanged("Type"); } } } 
    //    private string type;
    //    public int Portions { get { return portions; } set { if (value > 0) { portions = value; OnPropertyChanged("Portions"); } } }
    //    private int portions;
    //    public int Modifier { get { return modifier; } set { if (value >= 0) { modifier = value; OnPropertyChanged("Modifier"); } } }
    //    private int modifier;
    //    public int Id { get { return id; } set { if (value >= 0) { id = value; OnPropertyChanged("Id"); } } }
    //    private int id;
    //    public ObservableCollection<string> FoodTypes { get; set; }
    //    public Food(string name)
    //    {
    //        Name = name;
    //        modifier = 1;
    //        Portions = 1;
    //    }
    //    public Food()
    //    { modifier = 1; Portions = 1; }
    //    public event PropertyChangedEventHandler? PropertyChanged;
    //    public void OnPropertyChanged([CallerMemberName] string prop = "")
    //    {
    //        if (PropertyChanged != null)
    //            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    //    }
    //}
    //public class MealFilling : INotifyPropertyChanged
    //{
    //    public int Id { get; set; }        
    //    public string? Type { get; set; }        
    //    public string? FoodListOrder { get; set; } = "";
    //    public int FoodQuantity { get { return FoodTypeList.Count(); } set { if (value > 0 && value <= 6) { ChangeFoodquant(value); OnPropertyChanged("FoodQuantity"); } } }
    //    public ObservableCollection<Food> FoodTypeList { get; set; }= new();
    //    public List<Food> FoodList { get; set; } = new();
    //    public int Day_Id { get; set; }
    //    public void AddFood(Food food)

    //    {
    //        FoodList.Add(food);
    //        FoodListOrder += food.Id;
    //        FoodListOrder += " ";
    //    }
    //    public void ChangeFoodquant(int quant)
    //    {
    //        while(FoodTypeList.Count != quant) 
    //        {
    //            if (FoodTypeList.Count > quant)
    //            {
    //                FoodTypeList.RemoveAt(FoodTypeList.Count - 1);
    //            }
    //            else 
    //            {
    //                FoodTypeList.Add(new Food()); 
    //            }
    //        }
    //    }
    //    public void Normalize()
    //    {
    //        List<int> Ids = new List<int>();
    //        foreach (string a in FoodListOrder.Split(' ').ToList())
    //            if (a != "")
    //                Ids.Add(int.Parse(a));
    //        if (FoodList.Count != Ids.Count)
    //        {
    //            List<Food> temp = new List<Food>();
    //            foreach (int i in Ids)
    //            {
    //                var s = from f in FoodList where f.Id == i select f;
    //                temp.Add(s.First());
    //            }
    //            FoodList = temp;
    //        }
    //    }
    //    public event PropertyChangedEventHandler? PropertyChanged;
    //    public void OnPropertyChanged([CallerMemberName] string prop = "")
    //    {
    //        if (PropertyChanged != null)
    //            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    //    }
    //}
    //public class Day
    //{
    //    public int Id { get; set; }
    //    public DateOnly Date { get; set; }
    //    public MealFilling BreakFast { get; set; }        
    //    public MealFilling Lunch { get; set; }
    //    public MealFilling Dinner { get; set; }
    //    public int Week_Id { get; set; }
    //}
    //public class Week
    //{
    //    public int Id { get; set; }
    //    public DateOnly FirstDay { get; set; }
    //    public DateOnly LastDay { get; set; }
    //    public List<Day> DayList { get; set; } = new();
    //}

}
