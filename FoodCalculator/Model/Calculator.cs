using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodCalculator.Model
{
    public class Calculator
    {
        Random rnd = new Random();
        private  List<Food> FoodList { get; set; } = new List<Food>();
        private WeekParameters WeekParams { get; set; }
        private List<string> FoodTypes;
        private Food[][] SortedFoods { get; set; }
        private Queue<Food>[] FoodQueue;// TODO delete kall
        private bool[][] WasThisFoodUsedToRoll;
        ObservableCollection<DayTemplate> DayTemplates { get; set; }
        private Week WeekForCalculating;
        public Calculator(Week week,IEnumerable<Food> foodlist, ObservableCollection<DayTemplate> dayTemplates, IEnumerable<string> foodtypes)
        {
            FoodList = foodlist.ToList();
            DayTemplates = dayTemplates;
            WeekForCalculating = week;
            FoodTypes = foodtypes.ToList();
            WeekParams = new WeekParameters(DayTemplates.ToList(), FoodTypes, FoodList);
            SortedFoods = new Food[FoodTypes.Count()][];
            FoodQueue = new Queue<Food>[FoodTypes.Count()];
            for (int i = 0; i < SortedFoods.Length; i++)
            {
                FoodQueue[i] = new();
                var temp = from fl in FoodList
                           where fl.Type == FoodTypes[i]
                           select fl;
                SortedFoods[i] = new Food[temp.Count()];
                SortedFoods[i] = temp.ToArray();
            }
        }
        public void Calculate()
        {
            if (WeekParams.IsCalculatable)
            {
                List<Food> tempF = new();
                int tempInt;
                WeekForCalculating.ClearAllFood();
                for(int i = 0;i<FoodTypes.Count();i++)
                {
                    FoodQueue[i] = CreateFoodQueueWithSpecificLength(i, WeekParams.AmmountOfEachType[i]);
                }
                for(int i = 0;i<7;i++)
                {
                    tempF.Clear();
                    for(int j = 0;j< DayTemplates[i].Breakfast.Count();j++)
                    {
                        tempInt = FoodTypes.IndexOf(DayTemplates[i].Breakfast[j].Value);
                        tempF.Add(FoodQueue[tempInt].Dequeue());                        
                    }
                    InputMF(WeekForCalculating.DaysOfTheWeek[i].Breakfast, tempF);



                    tempF.Clear();
                    for (int j = 0; j < DayTemplates[i].Lunch.Count(); j++)
                    {
                        tempInt = FoodTypes.IndexOf(DayTemplates[i].Lunch[j].Value);
                        tempF.Add(FoodQueue[tempInt].Dequeue());
                    }
                    InputMF(WeekForCalculating.DaysOfTheWeek[i].Lunch, tempF);



                    tempF.Clear();
                    for (int j = 0; j < DayTemplates[i].Dinner.Count(); j++)
                    {
                        tempInt = FoodTypes.IndexOf(DayTemplates[i].Dinner[j].Value);
                        tempF.Add(FoodQueue[tempInt].Dequeue());
                    }
                    InputMF(WeekForCalculating.DaysOfTheWeek[i].Dinner, tempF);

                }
            }
            else
            {
                MessageBox.Show(WeekParams.ErrorMessage.ToString());
            }
        }
        private Queue<Food> CreateFoodQueueWithSpecificLength(int foodTypeIndex,int length)
        {
            List<Food> list = new List<Food>();
            Queue<Food> res = new Queue<Food>();
            int rndInd;
            a:
            list.Clear();
            while (list.Count <length)
            {
                b:
                rndInd = rnd.Next(SortedFoods[foodTypeIndex].Length);
                if(list.Count + SortedFoods[foodTypeIndex][rndInd].Portions <= length)
                {
                    for (int i = 0; i < SortedFoods[foodTypeIndex][rndInd].Portions; i++)
                        list.Add(SortedFoods[foodTypeIndex][rndInd]);
                }
                else if (WeekParams.EveryPossiblePortionSizeOfEachType[foodTypeIndex].Any(s=>0<=length-(list.Count + s)))
                {
                    goto b;
                }
                else { goto a; }
            }
            if(list.Count !=length)
            {
                goto a;
            }
            else
            {
                for (int i = 0;i < list.Count;i++)
                {
                    res.Enqueue(list[i]);
                }
            }            
            return res;
        }        
        private void InputMF(MealFilling mf,IEnumerable<Food> foods)
        {
            foreach (var food in foods)
            {
                mf.AddFood(food);
            }
            mf.UpdateOutputValue();
        }
        private class WeekParameters
        {
            public List<Food> FoodList { get; set; }
            public List<StringWrapper> EveryFoodPiece { get; set; } = new List<StringWrapper>();
            public List<int> AmmountOfEachType { get; set; }
            public List<int>[] EveryPossiblePortionSizeOfEachType { get; set; } 
            public bool IsCalculatable = true;
            public StringBuilder ErrorMessage { get; set; } = new StringBuilder("Not enough food with type: ");
            public WeekParameters(List<DayTemplate> dayTemplates,List<string> foodtypes, List<Food> foodList)
            {
                FoodList = foodList;
                for (int i = 0; i < 7; i++)
                {
                    EveryFoodPiece.AddRange(dayTemplates[i].Breakfast);
                    EveryFoodPiece.AddRange(dayTemplates[i].Lunch);
                    EveryFoodPiece.AddRange(dayTemplates[i].Dinner);
                }
                AmmountOfEachType = new List<int>();
                EveryPossiblePortionSizeOfEachType = new List<int>[foodList.Count()];
                for (int i = 0;i<foodtypes.Count();i++)
                {                    
                    AmmountOfEachType.Add((from f in EveryFoodPiece
                                           where f.ToString().ToLower() == foodtypes[i].ToString().ToLower()
                                           select f).Count());
                    List<int> list = new List<int>();
                    list.AddRange((from f in FoodList
                                  where f.Type.ToLower() == foodtypes[i].ToString().ToLower()
                                  select f.Portions).Distinct().ToList());
                    EveryPossiblePortionSizeOfEachType[i] = list;
                    bool temp = false;
                    IsThereEnoughFood(new Node(AmmountOfEachType.Last(), list), ref temp);                      
                    if(!temp)
                    {
                        IsCalculatable = false;
                        ErrorMessage.Append( foodtypes[i].ToString() + ", ");
                    }
                }
                ErrorMessage.Remove(ErrorMessage.Length-2, 2);
            }

    


            #region BinaryTreeChecking
            void IsThereEnoughFood(Node node, ref bool b)
            {
                if (b)
                {
                    return;
                }
                if (node.Nodes.Count == 0)
                {
                    if (node.Value == 0)
                    {
                        b = true;
                        return;
                    }
                }
                foreach (var nod in node.Nodes)
                {
                    IsThereEnoughFood(nod, ref b);
                }
            }
            class Node
            {
                public List<Node> Nodes = new List<Node>();
                public int Value;
                public Node(int val, List<int> n)
                {
                    Value = val;
                    if (Value > 0)
                    {
                        CreateNodes(n);
                    }
                }
                public void CreateNodes(List<int> n)
                {
                    foreach (var num in n)
                    {
                        Nodes.Add(new Node((Value - num), n));
                    }
                }
            }
            #endregion
        }
    }
}