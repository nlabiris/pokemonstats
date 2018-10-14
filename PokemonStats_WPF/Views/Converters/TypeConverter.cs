using System;
using System.Globalization;
using System.Windows.Data;

namespace PokemonStats_WPF.Views.Converters {
    [ValueConversion(typeof(string), typeof(string))]
    public class TypeConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if ((string)values[1] == string.Empty)
                return string.Format("{0}", (string)values[0]);
            else
                return string.Format("{0}/{1}", (string)values[0], (string)values[1]);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
