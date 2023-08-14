using FoodCalculator.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FoodCalculator.Converters
{
    public class DayOfTheWeekToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string day = value.ToString();
            switch (day)
            {
                case "MON": return 0;
                case "TUE": return 1;
                case "WED": return 2;
                case "THU": return 3;
                case "FRI": return 4;
                case "SAT": return 5;
                case "SUN": return 6;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
