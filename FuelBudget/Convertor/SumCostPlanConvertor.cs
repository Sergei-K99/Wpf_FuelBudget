using FuelBudget.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FuelBudget.Convertor
{
    public class SumCostPlanConvertor :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<FuelDetail> items)
            {
                double sum = items.Sum(item => item.FuelPlanCost);
                return sum.ToString(); // Возвращаем строковое представление суммы
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
