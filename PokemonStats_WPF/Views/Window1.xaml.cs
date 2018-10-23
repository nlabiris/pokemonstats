using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PokemonStats_WPF.Helper;
using PokemonStats_WPF.ViewModels;
using PokemonStats_WPF.Models;

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
                imgFormIcon.Source = Utility.ByteToImage(form.Icon);
                tbxFormName.Text = form.FormName;
                tbxFormName.Margin = new Thickness(5, 10, 0, 0);
                wrapPanelForm.Children.Add(imgFormIcon);
                wrapPanelForm.Children.Add(tbxFormName);
            }
        }

        private void AddPokemonEvolutions() {
            int column = 1;
            int previousStage = 0;
            int i = 0;
            List<Border> borders = new List<Border>();
            foreach (var evolution in _vm.SelectedPokemon.Evolutions) {
                Border border = new Border {
                    VerticalAlignment = VerticalAlignment.Center,
                    BorderBrush = new SolidColorBrush(Colors.Brown),
                    Background = new SolidColorBrush(Colors.Aquamarine)
                };
                borders.Add(border);

                if (previousStage == evolution.Stage) {
                    column++;
                }

                border.SetValue(Grid.RowProperty, evolution.Stage);
                border.SetValue(Grid.ColumnProperty, column);
                gridEvolution.Children.Add(border);

                i++;

                WrapPanel wrapPanel = new WrapPanel();

                Image imgIcon = new Image {
                    Source = Utility.ByteToImage(evolution.Icon)
                };

                StackPanel stackPanel = new StackPanel();

                TextBlock tbxPokemonName = new TextBlock {
                    Text = evolution.Name
                };
                TextBlock tbxDescription = new TextBlock {
                    FontSize = 10,
                    Text = formatEvolutionTrigger(evolution)
                };

                stackPanel.Children.Add(tbxPokemonName);
                stackPanel.Children.Add(tbxDescription);
                wrapPanel.Children.Add(imgIcon);
                wrapPanel.Children.Add(stackPanel);
                border.Child = wrapPanel;

                previousStage = evolution.Stage;
            }
        }

        private string formatEvolutionTrigger(Evolution evolution) {
            string description = string.Empty;
            if (evolution.EvolutionTrigger != null) {
                if (evolution.EvolutionTrigger == "Level up") {
                    if (evolution.MinimumLevel != 0) {
                        description = string.Format("Level up, starting at level {0}", evolution.MinimumLevel);
                        if (evolution.Gender != null) {
                            description += string.Format(", {0} only", evolution.Gender);
                        }
                        if (evolution.RelativePhysicalStats != null) {
                            switch (evolution.RelativePhysicalStats) {
                                case 1:
                                    description += ", when Attack > Defense";
                                    break;
                                case -1:
                                    description += ", when Attack < Defense";
                                    break;
                                case 0:
                                    description += ", when Attack = Defense";
                                    break;
                            }
                        }
                        if (evolution.PartyType != null) {
                            description += string.Format(", with a {0}-type Pokemon in the party", evolution.PartyType);
                        }
                        if (evolution.NeedsOverworldRain != false) {
                            description += ", while it is raining outside of battle";
                        }
                        if (evolution.TurnUpsideDown != false) {
                            description += ", with the 3DS turned upside-down";
                        }
                    } else if (evolution.MinimumHappiness != 0) {
                        description = string.Format("Level up, with at least {0} happiness", evolution.MinimumHappiness);
                    } else if (evolution.Location != null) {
                        description = string.Format("Level up, around {0}", evolution.Location);
                    } else if (evolution.TimeOfDay != null) {
                        description = string.Format("Level up, during the {0}", evolution.TimeOfDay);
                        if (evolution.MinimumLevel != 0) {
                            description += string.Format(", starting at level {0}", evolution.MinimumLevel);
                        }
                        if (evolution.HeldItem != null) {
                            description += string.Format(", while holding a {0}", evolution.HeldItem);
                        }
                    } else if (evolution.KnownMove != null) {
                        description = string.Format("Level up, while knowing {0}", evolution.KnownMove);
                    } else if (evolution.KnownMoveType != null) {
                        description = string.Format("Level up, knowing a {0}-type move", evolution.KnownMoveType);
                        if (evolution.MinimumAffection != 0) {
                            description += string.Format(", with at least {0} affection in Pokemon-Amie", evolution.MinimumAffection);
                        }
                    } else if (evolution.MinimumBeauty != 0) {
                        description = string.Format("Level up, with at least {0} beauty", evolution.MinimumBeauty);
                    } else if (evolution.PartySpecies != null) {
                        description = string.Format("Level up, with {0} in the party", evolution.PartySpecies);
                    }
                }

                if (evolution.EvolutionTrigger == "Use item") {
                    if (evolution.TriggerItem != null) {
                        description = string.Format("Use a {0}", evolution.TriggerItem);
                        if (evolution.Gender != null) {
                            description += string.Format(", {0} only", evolution.Gender);
                        }
                    }
                }

                if (evolution.EvolutionTrigger == "Trade") {
                    if (evolution.HeldItem != null) {
                        description = string.Format("Trade, while holding a {0}", evolution.HeldItem);
                    } else if (evolution.TradeSpecies != null) {
                        description = string.Format("Trade, in exchange for {0}", evolution.TradeSpecies);
                    }
                }

                if (evolution.EvolutionTrigger == "Shed") {

                }
            }
            return description;
        }
    }
}
