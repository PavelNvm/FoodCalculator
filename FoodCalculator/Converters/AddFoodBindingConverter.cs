using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FoodCalculator.Converters
{
    public class AddFoodBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int portions;
            int.TryParse(values[2].ToString(), out portions);
            if (values[1] != null)
                return new Food() { Name = values[0].ToString(), Type = values[1].ToString(), Portions = portions };
            else return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
