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

namespace FoodCalculator
{
    /// <summary>
    /// Interaction logic for AddFoodWindow.xaml
    /// </summary>
    public partial class AddFoodWindow : Window
    {
        public AddFoodWindow()
        {
            InitializeComponent();
            Closed += AddFoodWindow_Closed;
            //FoodCalc foodCalc = new FoodCalc();
            //DataContext = foodCalc;
        }

        private void AddFoodWindow_Closed(object? sender, EventArgs e)
        {
            if (Application.Current.Windows.Count == 1)
            {
                Application.Current.Shutdown();
            }
        }

        private void FoodList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
