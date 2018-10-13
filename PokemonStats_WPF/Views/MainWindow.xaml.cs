using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokemonStats_WPF.ViewModels;

namespace PokemonStats_WPF.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private PokemonViewModel _pokemonViewModel = null;

        public MainWindow() {
            InitializeComponent();
            _pokemonViewModel = new PokemonViewModel();
            dg.ItemsSource = _pokemonViewModel.RetrieveAllPokemon();
        }

        // This snippet is much safer in terms of preventing unwanted
        // Exceptions because of missing [DisplayNameAttribute].
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor) {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
        }
    }
}
