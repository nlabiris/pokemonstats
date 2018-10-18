using System;
using System.Globalization;
using System.Windows.Data;
using PokemonStats_WPF.Helper;

namespace PokemonStats_WPF.Views.Converters {
    [ValueConversion(typeof(string), typeof(string))]
    public class WeightConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double kg = (int)value / 10.0;
            double lb = Utility.KgToLb(kg);
            return string.Format("{0} kg / {1} lb", kg, Math.Round(lb,1));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
