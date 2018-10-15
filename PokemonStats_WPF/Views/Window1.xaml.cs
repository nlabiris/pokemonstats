﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using PokemonStats_WPF.ViewModels;
using PokemonStats_WPF.Models;

namespace PokemonStats_WPF.Views {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        public Window1(Pokemon p, PokemonViewModel _pokemonViewModel) {
            InitializeComponent();
            DataContext = _pokemonViewModel;
            pokemonCmb.ItemsSource = _pokemonViewModel.Pokemons;
            pokemonCmb.SelectedItem = p;
        }
    }
}
