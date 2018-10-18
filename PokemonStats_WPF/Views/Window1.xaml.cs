using System.Windows;
using PokemonStats_WPF.ViewModels;

namespace PokemonStats_WPF.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        public Window1(PokemonViewModel _pokemonViewModel) {
            InitializeComponent();
            _pokemonViewModel.SelectedPokemon.Forms = _pokemonViewModel.RetrievePokemonForm(_pokemonViewModel.SelectedPokemon.IndexNumber);
            _pokemonViewModel.SelectedPokemonWindowTitle = string.Format("{0} - #{1}", _pokemonViewModel.SelectedPokemon.Name, _pokemonViewModel.SelectedPokemon.IndexNumber);
            DataContext = _pokemonViewModel;
        }
    }
}
