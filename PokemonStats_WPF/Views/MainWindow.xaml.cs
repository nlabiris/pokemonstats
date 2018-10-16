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

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            Pokemon p = dg.SelectedItem as Pokemon;
            Window1 specificPokemonWindow = new Window1(p, _pokemonViewModel);
            specificPokemonWindow.Show();
        }

        private void searchTbx_TextChanged(object sender, TextChangedEventArgs e) {
            _pokemonViewModel.Pokemons.Refresh();
        }
    }
}
