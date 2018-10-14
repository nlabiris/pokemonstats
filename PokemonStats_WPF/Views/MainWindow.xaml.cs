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
using PokemonStats_WPF.Models;

namespace PokemonStats_WPF.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private PokemonViewModel _pokemonViewModel = null;

        public MainWindow() {
            InitializeComponent();
            _pokemonViewModel = new PokemonViewModel();
            DataContext = _pokemonViewModel;
            dg.ItemsSource = _pokemonViewModel.Pokemons;
            _pokemonViewModel.Pokemons.Filter = _pokemonViewModel.PokemonsFilter;
        }

        // This snippet is much safer in terms of preventing unwanted
        // Exceptions because of missing [DisplayNameAttribute].
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor) {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
            //Cancel columns you don't want to generate
            switch (e.Column.Header.ToString()) {
                case "Details":
                case "SpeciesSummary":
                    e.Cancel = true;
                    break;
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e) {
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            Window1 specificPokemonWindow = new Window1();
            specificPokemonWindow.Show();
        }

        private void searchTbx_TextChanged(object sender, TextChangedEventArgs e) {
            _pokemonViewModel.Pokemons.Refresh();
        }
    }
}
