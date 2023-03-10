using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace FoodCalculator
{
    internal class FoodCalcer : INotifyPropertyChanged
    {
        public ObservableCollection<Food> FoodList { get { return _foodList; } set { _foodList = value; OnPropertyChanged("FoodList"); } }
        private ObservableCollection<Food> _foodList = new ObservableCollection<Food>();
        public int InputPortionQuantity { get { return _inputPortionQuantity; } set { if (value > 0) _inputPortionQuantity = value; OnPropertyChanged("InputPortionQuantity"); } }
        private int _inputPortionQuantity;
        public RelayCommand NameInput { get; set; }
        public RelayCommand TestCommand { get; set; }
        public RelayCommand Increment { get; set; }
        public RelayCommand Decrement { get; set; }
        public RelayCommand PortionsIncrement { get; set; }
        public RelayCommand PortionsDecrement { get; set; }
        public RelayCommand Delete { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public FoodCalcer()
        {
            FoodList.CollectionChanged += FoodList_CollectionChanged;
            InputPortionQuantity = 1;
            FoodContext FoodDB = new FoodContext();
            

            PortionsIncrement = new RelayCommand(obj =>
            {
                InputPortionQuantity++;
            });
            PortionsDecrement = new RelayCommand(obj =>
            {
                InputPortionQuantity--;
            });
            Delete = new RelayCommand(obj =>
            {
                int.TryParse(obj.ToString(), out int number);
                for (int i = FoodList.Count - 1; i > number; i--)
                {
                    FoodList[i].Id--;
                }
                var a = FoodDB.FoodList.Find(number) ?? null!;
                FoodDB.FoodList.Remove(a);
                FoodDB.SaveChanges();
                //FoodList.RemoveAt(number);
            });
            Increment = new RelayCommand(obj =>
            {
                int.TryParse(obj.ToString(), out int number);
                FoodList[number].Modifier++;

            });
            Decrement = new RelayCommand(obj =>
            {
                int.TryParse(obj.ToString(), out int number);
                FoodList[number].Modifier--;

            });
            NameInput = new RelayCommand(obj =>
            {
                object[] strings = obj as object[] ?? null!;
                if (strings.Any(item => item == null || item.ToString() == ""))
                    return;
                Food food = new Food();
                food.Name = strings[0].ToString() ?? "DefaultFood";
                food.Type = ((TextBlock)strings[1]).Text;
                int.TryParse(strings[2].ToString(), out int portionsInt);
                food.Portions = portionsInt;
                FoodList.Add(food);
                if (FoodList.Count == 1)
                {
                    FoodList[0].Id = 0;
                }
                if (FoodList.Count >= 2)
                {
                    FoodList[FoodList.Count - 1].Id = FoodList[FoodList.Count - 2].Id + 1;
                }
                FoodDB.FoodList.Add(food);
                FoodDB.SaveChanges();

            });
            TestCommand = new RelayCommand(obj =>
            {
                FoodList.Add(new Food() { Id = 0, Name = "kaWa", Type = "KaWa" });
                FoodList.Add(new Food() { Id = 1, Name = "eggs", Type = "Eggs" });
                FoodList.Add(new Food() { Id = 2, Name = "kotleta", Type = "Main",Portions=3 });
                FoodList.Add(new Food() { Id = 3, Name = "seledka pod shuboi", Type = Food.FoodType.Salad.ToString() });
                FoodList.Add(new Food() { Id = 4, Name = "jarennaya chicken", Type = Food.FoodType.Main.ToString(),Portions=4 });
                FoodList.Add(new Food() { Id = 5, Name = "Winters salad", Type = Food.FoodType.Salad.ToString() });
                FoodList.Add(new Food() { Id = 6, Name = "rice", Type = Food.FoodType.Garnish.ToString(),Portions = 2 });
                FoodList.Add(new Food() { Id = 7, Name = "kartoxa", Type = Food.FoodType.Garnish.ToString(), Portions = 2 }) ;
            });
            Linker.ViewModels.Add(this);
            FoodDB.Database.EnsureCreated();
            FoodDB.FoodList.Load();
            FoodList = FoodDB.FoodList.Local.ToObservableCollection();
            //FoodList.Add(new Food("priva") { Type="Soup",Id=0});
        }

        private void FoodList_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove: // если добавление


                    break;
            }
        }
    }
}
