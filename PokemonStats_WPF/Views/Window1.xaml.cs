using System.Windows;
using PokemonStats_WPF.ViewModels;
using PokemonStats_WPF.Models;

namespace PokemonStats_WPF.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        public Window1(Pokemon p, PokemonViewModel _pokemonViewModel) {
            InitializeComponent();
            p.Forms = _pokemonViewModel.RetrievePokemonForm(p.IndexNumber);
            DataContext = _pokemonViewModel;
            pokemonCmb.ItemsSource = _pokemonViewModel.Pokemons;
            pokemonCmb.SelectedItem = p;
        }
    }
}
