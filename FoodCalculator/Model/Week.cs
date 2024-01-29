using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Model
{
    public class Week : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateOnly FirstDay { get { return firstDay; } set { firstDay = value; OnPropertyChanged(nameof(WeekSpan)); } }
        private DateOnly firstDay;
        public DateOnly LastDay { get { return lastDay; } set { lastDay = value; OnPropertyChanged(nameof(WeekSpan)); } }
        private DateOnly lastDay;

        public Day[] DaysOfTheWeek { get; set; } = new Day[7] {new Day(), new Day(), new Day(), new Day(), new Day(), new Day(), new Day() };
        public Week((DateOnly, DateOnly) a) 
        {
            FirstDay = a.Item1;
            LastDay = a.Item2;
            for (int i =0;i<7;i++)
            {
                DaysOfTheWeek[i].Week_Id = Id;
            }
        }
        public Week()
        {
            for (int i = 0; i < 7; i++)
            {
                DaysOfTheWeek[i].Week_Id = Id;
            }
        }
        public string WeekSpan { get { return FirstDay.ToString() + " - " + LastDay.ToString(); }  }
        
        public void equate(Week week)
        {
            FirstDay = week.FirstDay; 
            LastDay = week.LastDay;
            Id = week.Id;
            for (int i = 0;i<7;i++)
            {
                DaysOfTheWeek[i].equate(week.DaysOfTheWeek[i]);
            }

        }        
        internal void ClearAllFood()
        {
            for(int i=0;i<7;i++)
            {
                DaysOfTheWeek[i].ClearAllFood();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
