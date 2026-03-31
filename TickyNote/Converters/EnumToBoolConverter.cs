using System.Globalization;
using System.Windows.Data;

namespace TickyNote
{
    public class EnumToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return false;

            var selectedValue = values[0];
            var itemValue = values[1];

            if (selectedValue == null || itemValue == null)
                return false;

            return selectedValue.Equals(itemValue);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
