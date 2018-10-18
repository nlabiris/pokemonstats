using System;
using System.Globalization;
using System.Windows.Data;
using PokemonStats_WPF.Helper;

namespace PokemonStats_WPF.Views.Converters {
    [ValueConversion(typeof(string), typeof(string))]
    public class HeightConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double meters = (int)value / 10.0;
            double feet = Utility.MetersToFeet(meters);
            double inches = Utility.FeetToInches(feet - Math.Truncate(feet));
            return string.Format("{0} m / {1}'{2}\"", meters, Math.Truncate(feet), Math.Round(inches, 1));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
