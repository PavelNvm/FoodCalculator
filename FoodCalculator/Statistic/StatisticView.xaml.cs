using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FoodCalculator.Statistic
{
    /// <summary>
    /// Логика взаимодействия для StatisticView.xaml
    /// </summary>
    public partial class StatisticView : Window
    {
        public StatisticView()
        {
            InitializeComponent();
            Loaded += StatisticView_Loaded;
            Closed += StatisticView_Closed;
        }

        private void StatisticView_Closed(object? sender, EventArgs e)
        {
            if (Application.Current.Windows.Count == 1)
            {
                Application.Current.Shutdown();
            }
        }

        private void StatisticView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
