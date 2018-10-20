using System.Windows;
using System.Windows.Controls;
using PokemonStats_WPF.ViewModels;
using PokemonStats_WPF.Models;

namespace PokemonStats_WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private PokemonViewModel _pokemonViewModel = null;

        public MainWindow() {
            InitializeComponent();
            searchTbx.Focus();
            _pokemonViewModel = new PokemonViewModel();
            _pokemonViewModel.MainWindowTitle = "Pokemon Stats";
            dg.ItemsSource = _pokemonViewModel.Pokemons;
            _pokemonViewModel.Pokemons.Filter = _pokemonViewModel.PokemonsFilter;
            DataContext = _pokemonViewModel;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            _pokemonViewModel.SelectedPokemon = dg.SelectedItem as Pokemon;
            Window1 specificPokemonWindow = new Window1(_pokemonViewModel);
            specificPokemonWindow.Show();
        }

        private void searchTbx_TextChanged(object sender, TextChangedEventArgs e) {
            _pokemonViewModel.Pokemons.Refresh();
        }
    }
}
