using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using PokemonStats_WPF.Models;
using PokemonStats_WPF.Helper;
using PokemonStats_WPF.DataAccess;
using System.Windows.Data;

namespace PokemonStats_WPF.ViewModels {
    public class PokemonViewModel : DataWorker, INotifyPropertyChanged {
        private string _filterString;
        private string _mainWindowTitle;
        private string _selectedPokemonWindowTitle;
        private ICollectionView _pokemonsView;
        private Pokemon _selectedPokemon;

        public string FilterString {
            get { return _filterString; }
            set {
                if (_filterString != value) {
                    _filterString = value;
                    OnPropertyChanged("FilterString");
                    _pokemonsView.Refresh();
                }
            }
        }

        public string MainWindowTitle {
            get { return _mainWindowTitle; }
            set {
                if (_mainWindowTitle != value) {
                    _mainWindowTitle = value;
                    OnPropertyChanged("MainWindowTitle");
                }
            }
        }

        public string SelectedPokemonWindowTitle {
            get { return _selectedPokemonWindowTitle; }
            set {
                if (_selectedPokemonWindowTitle != value) {
                    _selectedPokemonWindowTitle = value;
                    OnPropertyChanged("SelectedPokemonWindowTitle");
                }
            }
        }

        public ICollectionView Pokemons {
            get { return _pokemonsView; }
            set {
                if (_pokemonsView != value) {
                    _pokemonsView = value;
                    OnPropertyChanged("Pokemons");
                }
            }
        }

        public Pokemon SelectedPokemon {
            get { return _selectedPokemon; }
            set {
                if (_selectedPokemon != value) {
                    _selectedPokemon = value;
                    OnPropertyChanged("Pokemon");
                }
            }
        }

        public PokemonViewModel() {
            List<Pokemon> pokemons = RetrieveAllPokemon();
            _pokemonsView = CollectionViewSource.GetDefaultView(pokemons);
        }

        public bool PokemonsFilter(object item) {
            if (String.IsNullOrEmpty(_filterString)) {
                return true;
            } else {
                return (
                    (int.TryParse(_filterString, out int n) ? (item as Pokemon).IndexNumber == n : false) ||
                    ((item as Pokemon).Name.IndexOf(_filterString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as Pokemon).Type1.IndexOf(_filterString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as Pokemon).Type2.IndexOf(_filterString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as Pokemon).Color.IndexOf(_filterString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as Pokemon).Shape.IndexOf(_filterString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as Pokemon).Ability1.IndexOf(_filterString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as Pokemon).Ability2.IndexOf(_filterString, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as Pokemon).HiddenAbility.IndexOf(_filterString, StringComparison.OrdinalIgnoreCase) >= 0)
                );
            }
        }

        public List<Pokemon> RetrieveAllPokemon() {
            List<Pokemon> pokemons = new List<Pokemon>();
            using (IDbConnection connection = database.CreateOpenConnection()) {
                using (IDbCommand command = database.CreateCommand()) {
                    command.Connection = connection;
                    command.CommandText = Query.GetAllPokemon;
                    command.Prepare();
                    using (IDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            Pokemon pokemon = new Pokemon {
                                IndexNumber = reader.CheckValue<int>("index_number"),
                                Name = reader.CheckObject<string>("name"),
                                Genus = reader.CheckObject<string>("genus"),
                                Type1 = reader.CheckObject<string>("type1") ?? string.Empty,
                                Type2 = reader.CheckObject<string>("type2") ?? string.Empty,
                                Type1_Image = reader.CheckObject<byte[]>("type1_image"),
                                Type2_Image = reader.CheckObject<byte[]>("type2_image"),
                                Stats = new Stats {
                                    HP = reader.CheckValue<int>("hp"),
                                    Atk = reader.CheckValue<int>("atk"),
                                    Def = reader.CheckValue<int>("def"),
                                    SpAtk = reader.CheckValue<int>("sp_atk"),
                                    SpDef = reader.CheckValue<int>("sp_def"),
                                    Spe = reader.CheckValue<int>("spe"),
                                    EVYieldHP = reader.CheckValue<int>("ev_yield_hp"),
                                    EVYieldAtk = reader.CheckValue<int>("ev_yield_atk"),
                                    EVYieldDef = reader.CheckValue<int>("ev_yield_def"),
                                    EVYieldSpAtk = reader.CheckValue<int>("ev_yield_sp_atk"),
                                    EVYieldSpDef = reader.CheckValue<int>("ev_yield_sp_def"),
                                    EVYieldSpe = reader.CheckValue<int>("ev_yield_spe")
                                },
                                EggGroups = reader.CheckObject<string>("egg_groups"),
                                Color = reader.CheckObject<string>("color"),
                                Shape = reader.CheckObject<string>("shape"),
                                Shape_Image = reader.CheckObject<byte[]>("shape_image"),
                                Habitat = reader.CheckObject<string>("habitat"),
                                Habitat_Image = reader.CheckObject<byte[]>("habitat_image"),
                                Footprint = reader.CheckObject<byte[]>("footprint"),
                                EvolutionChainId = reader.CheckValue<int>("evolution_chain_id"),
                                EvolvesFromSpeciesId = reader.CheckValue<int>("evolves_from_species_id"),
                                GenderRate = reader.CheckValue<int>("gender_rate"),
                                CaptureRate = reader.CheckValue<int>("capture_rate"),
                                BaseHappiness = reader.CheckValue<int>("base_happiness"),
                                IsBaby = reader.CheckValue<bool>("is_baby"),
                                HatchCounter = reader.CheckValue<int>("hatch_counter"),
                                HatchSteps = reader.CheckValue<long>("hatch_steps"),
                                GrowthRate = reader.CheckObject<string>("growth_rate"),
                                Height = reader.CheckValue<int>("height"),
                                Weight = reader.CheckValue<int>("weight"),
                                BaseExperience = reader.CheckValue<int>("base_experience"),
                                Ability1 = reader.CheckObject<string>("ability1") ?? string.Empty,
                                Ability2 = reader.CheckObject<string>("ability2") ?? string.Empty,
                                HiddenAbility = reader.CheckObject<string>("hidden_ability") ?? string.Empty,
                                SpeciesSummary = reader.CheckObject<string>("species_summary")?.Replace("\r\n", " "),
                                Icon = reader.CheckObject<byte[]>("icon"),
                                Sprite = reader.CheckObject<byte[]>("sprite")
                            };
                            pokemons.Add(pokemon);
                        }
                    }
                } // Command

                foreach(Pokemon pokemon in pokemons) {
                    pokemon.Forms = new List<Form>();
                    using (IDbCommand command = database.CreateCommand()) {
                        command.Connection = connection;
                        command.CommandText = Query.GetSpecificForm;
                        command.Prepare();
                        command.AddWithValue("@species_id", pokemon.IndexNumber);
                        using (IDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                Form form = new Form {
                                    FormName = reader.CheckObject<string>("form_name") ?? string.Empty,
                                    PokemonName = reader.CheckObject<string>("pokemon_name") ?? string.Empty,
                                    IsDefault = reader.CheckValue<bool>("is_default"),
                                    IsBattleOnly = reader.CheckValue<bool>("is_battle_only"),
                                    IsMega = reader.CheckValue<bool>("is_mega"),
                                    Icon = reader.CheckObject<byte[]>("icon"),
                                    Sprite = reader.CheckObject<byte[]>("sprite")
                                };
                                pokemon.Forms.Add(form);
                            }
                        }
                    } // Command

                    pokemon.Evolutions = new List<Evolution>();
                    int stage = 0;
                    int i = 0;
                    using (IDbCommand command = database.CreateCommand()) {
                        command.Connection = connection;
                        command.CommandText = Query.GetSpecificEvolutionChain;
                        command.Prepare();
                        command.AddWithValue("@evolution_chain_id", pokemon.EvolutionChainId);
                        using (IDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                Evolution evolution = new Evolution {
                                    Name = reader.CheckObject<string>("name"),
                                    Icon = reader.CheckObject<byte[]>("icon"),
                                    EvolvesFromSpeciesId = reader.CheckValue<int>("evolves_from_species_id"),
                                    EvolutionTrigger = reader.CheckObject<string>("evolution_trigger"),
                                    TriggerItem = reader.CheckObject<string>("trigger_item"),
                                    MinimumLevel = reader.CheckValue<int>("minimum_level"),
                                    Gender = reader.CheckObject<string>("gender"),
                                    Location = reader.CheckObject<string>("location"),
                                    KnownMove = reader.CheckObject<string>("known_move"),
                                    KnownMoveType = reader.CheckObject<string>("known_move_type"),
                                    HeldItem = reader.CheckObject<string>("held_item"),
                                    IsBaby = reader.CheckValue<bool>("is_baby"),
                                    TimeOfDay = reader.CheckObject<string>("time_of_day"),
                                    MinimumHappiness = reader.CheckValue<int>("minimum_happiness"),
                                    MinimumAffection = reader.CheckValue<int>("minimum_affection"),
                                    MinimumBeauty = reader.CheckValue<int>("minimum_beauty"),
                                    RelativePhysicalStats = !DBNull.Value.Equals(reader["relative_physical_stats"]) ? (int)reader["relative_physical_stats"] : (int?)null,
                                    PartySpecies = reader.CheckObject<string>("party_species"),
                                    PartyType = reader.CheckObject<string>("party_type"),
                                    TradeSpecies = reader.CheckObject<string>("trade_species"),
                                    NeedsOverworldRain = reader.CheckValue<bool>("needs_overworld_rain"),
                                    TurnUpsideDown = reader.CheckValue<bool>("turn_upside_down"),
                                };
                                if (pokemon.Evolutions.Count > 0) {
                                    if (evolution.EvolvesFromSpeciesId != pokemon.Evolutions[i - 1].EvolvesFromSpeciesId) {
                                        stage++;
                                        i++;
                                    }
                                } else {
                                    stage++;
                                    i++;
                                }

                                if (evolution.IsBaby) {
                                    evolution.Stage = 0;
                                    stage = 0;
                                } else {
                                    evolution.Stage = stage;
                                }
                                
                                pokemon.Evolutions.Add(evolution);
                            }
                        }
                    } // Command
                } // foreach
            }
            return pokemons;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
