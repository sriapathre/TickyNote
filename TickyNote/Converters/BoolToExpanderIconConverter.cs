using System.Globalization;
using System.Windows.Data;

namespace TickyNote
{
    /// <summary>
    /// Expander for Add New Timer
    /// </summary>
    public class BoolToExpanderIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is true ? "▼" : "▶";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
