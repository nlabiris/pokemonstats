using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PokemonStats_WPF.Views.Converters {
    [ValueConversion(typeof(int), typeof(SolidColorBrush))]
    public class ProgressForegroundConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double progress = (double)value;
            SolidColorBrush foreground = new SolidColorBrush(Colors.LightGreen);

            if (progress >= 0 && progress < 50) {
                foreground = new SolidColorBrush(Colors.DarkRed);
            } else if (progress >= 50 && progress < 60) {
                foreground = new SolidColorBrush(Colors.Red);
            } else if (progress >= 60 && progress < 80) {
                foreground = new SolidColorBrush(Colors.OrangeRed);
            } else if (progress >= 80 && progress < 90) {
                foreground = new SolidColorBrush(Colors.OrangeRed);
            } else if (progress >= 90 && progress < 100) {
                foreground = new SolidColorBrush(Colors.Orange);
            } else if (progress >= 100 && progress < 110) {
                foreground = new SolidColorBrush(Colors.GreenYellow);
            } else if (progress >= 110 && progress < 130) {
                foreground = new SolidColorBrush(Colors.Green);
            } else if (progress >= 130 && progress < 180) {
                foreground = new SolidColorBrush(Colors.LightGreen);
            } else if (progress >= 180) {
                foreground = new SolidColorBrush(Colors.Cyan);
            }

            return foreground;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
