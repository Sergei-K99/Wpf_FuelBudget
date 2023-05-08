using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace FuelBudget.Convertor
{
    public class EmptyIntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double? id = (double)value;

            if (id!=null || id>0)
            {
                return Visibility.Visible; // В противном случае кнопка будет видима

            }
            else
            {
                return Visibility.Collapsed; // Если значение пусто или null, кнопка будет скрыта

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
