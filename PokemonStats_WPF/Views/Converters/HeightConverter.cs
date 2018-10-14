﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace PokemonStats_WPF.Views.Converters {
    [ValueConversion(typeof(string), typeof(string))]
    public class HeightConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return string.Format("{0} m", (int)value / 10.0);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}