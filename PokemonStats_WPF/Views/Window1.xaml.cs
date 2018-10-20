using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PokemonStats_WPF.ViewModels;

namespace PokemonStats_WPF.Views {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        PokemonViewModel _vm = null;

        public Window1(PokemonViewModel _pokemonViewModel) {
            _vm = _pokemonViewModel;
            InitializeComponent();
            _vm.SelectedPokemonWindowTitle = string.Format("{0} - #{1}", _vm.SelectedPokemon.Name, _vm.SelectedPokemon.IndexNumber);
            AddPokemonForms();
            AddPokemonEvolutions();
            DataContext = _vm;
        }

        private void AddPokemonForms() {
            if (_vm.SelectedPokemon.Forms.Count == 0) {
                gbForms.Visibility = Visibility.Hidden;
            }
            if (_vm.SelectedPokemon.Forms.Count > 4) {
                svForms.Height = 150;
            }
            foreach (var form in _vm.SelectedPokemon.Forms) {
                WrapPanel wrapPanelForm = new WrapPanel();
                panelForms.Children.Add(wrapPanelForm);
                Image imgFormIcon = new Image();
                TextBlock tbxFormName = new TextBlock();
                imgFormIcon.Source = ByteToImage(form.Icon);
                tbxFormName.Text = form.FormName;
                tbxFormName.Margin = new Thickness(5, 10, 0, 0);
                wrapPanelForm.Children.Add(imgFormIcon);
                wrapPanelForm.Children.Add(tbxFormName);
            }
        }

        private void AddPokemonEvolutions() {
            int row = 0;
            int column = 0;
            foreach (var evolution in _vm.SelectedPokemon.Evolutions) {
                Border border = new Border {
                    VerticalAlignment = VerticalAlignment.Center,
                    Padding = new Thickness(37),
                    Margin = new Thickness(5, 0, 5, 0),
                    BorderBrush = new SolidColorBrush(Colors.Brown),
                    Background = new SolidColorBrush(Colors.Aquamarine)
                };
                Grid.SetRow(border, 0);
                Grid.SetColumn(border, column);
                gridEvolution.Children.Add(border);

                WrapPanel wrapPanel = new WrapPanel();

                Image imgIcon = new Image {
                    Source = ByteToImage(evolution.Icon)
                };

                StackPanel stackPanel = new StackPanel {
                    Margin = new Thickness(17)
                };

                TextBlock tbxPokemonName = new TextBlock {
                    Text = evolution.Name
                };
                TextBlock tbxDescription = new TextBlock {
                    FontSize = 10
                };

                stackPanel.Children.Add(tbxPokemonName);
                stackPanel.Children.Add(tbxDescription);
                wrapPanel.Children.Add(imgIcon);
                wrapPanel.Children.Add(stackPanel);
                border.Child = wrapPanel;

                column++;
            }
        }

        private static ImageSource ByteToImage(byte[] imageData) {
            BitmapImage biImg = new BitmapImage();
            biImg.BeginInit();
            biImg.StreamSource = new MemoryStream(imageData);
            biImg.EndInit();
            return biImg as ImageSource;
        }
    }
}
