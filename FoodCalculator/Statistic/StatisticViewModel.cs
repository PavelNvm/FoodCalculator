using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator.Statistic
{
    internal class StatisticViewModel : INotifyPropertyChanged
    {
        public FCStatistic Statistic { get { return _statistic; } set { _statistic = value ?? new FCStatistic(); } }
        private FCStatistic _statistic;
        public RelayCommand TestCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public StatisticViewModel() 
        {
            StatisticContext DB = new StatisticContext(); 
            Statistic = new FCStatistic();
            TestCommand = new RelayCommand(obj =>
            {
                //try
                //{
                var DataSet = ((FoodCalcer)(Linker.ViewModels.Where(item => item.GetType().Name == "FoodCalcer").FirstOrDefault())).FoodList;
                    DB.StatRecords.Add(new StatRecord(DataSet.FirstOrDefault() ?? null!, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)));
                    DB.StatRecords.Add(new StatRecord(DataSet.LastOrDefault() ?? null!, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute + 1, DateTime.Now.Second)));
                DB.SaveChanges();
                //}
                //catch { }
            });
            Linker.ViewModels.Add(this);
            DB.Database.EnsureCreated();
            DB.StatRecords.Load();
            Statistic.StatDataList = DB.StatRecords.Local.ToObservableCollection();
        }
    }
}
