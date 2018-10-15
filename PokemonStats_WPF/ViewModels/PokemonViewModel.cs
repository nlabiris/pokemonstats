using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonStats_WPF.Models;
using PokemonStats_WPF.Helper;
using PokemonStats_WPF.DataAccess;
using System.Windows.Data;

namespace PokemonStats_WPF.ViewModels {
    public class PokemonViewModel : DataWorker, INotifyPropertyChanged {
        private string _filterString;
        private ICollectionView _pokemonsView;

        public string FilterString {
            get { return _filterString; }
            set {
                _filterString = value;
                OnPropertyChanged("FilterString");
                _pokemonsView.Refresh();
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
                            Pokemon p = new Pokemon {
                                IndexNumber = reader.CheckValue<int>("pokemon_id"),
                                Name = reader.CheckObject<string>("name"),
                                Type1 = reader.CheckObject<string>("type1") ?? "",
                                Type2 = reader.CheckObject<string>("type2") ?? "",
                                Type1_Image = reader.CheckObject<byte[]>("type1_image"),
                                Type2_Image = reader.CheckObject<byte[]>("type2_image"),
                                HP = reader.CheckValue<int>("hp"),
                                Atk = reader.CheckValue<int>("atk"),
                                Def = reader.CheckValue<int>("def"),
                                SpAtk = reader.CheckValue<int>("sp_atk"),
                                SpDef = reader.CheckValue<int>("sp_def"),
                                Spe = reader.CheckValue<int>("spe"),
                                Color = reader.CheckObject<string>("color"),
                                Shape = reader.CheckObject<string>("shape"),
                                Habitat = reader.CheckObject<string>("habitat"),
                                GenderRate = reader.CheckValue<int>("gender_rate"),
                                CaptureRate = reader.CheckValue<int>("capture_rate"),
                                BaseHappiness = reader.CheckValue<int>("base_happiness"),
                                GrowthRate = reader.CheckObject<string>("growth_rate"),
                                Height = reader.CheckValue<int>("height"),
                                Weight = reader.CheckValue<int>("weight"),
                                BaseExperience = reader.CheckValue<int>("base_experience"),
                                Ability1 = reader.CheckObject<string>("ability1") ?? "",
                                Ability2 = reader.CheckObject<string>("ability2") ?? "",
                                HiddenAbility = reader.CheckObject<string>("hidden_ability") ?? "",
                                SpeciesSummary = reader.CheckObject<string>("species_summary")?.Replace("\r\n", " "),
                                Icon = reader.CheckObject<byte[]>("icon"),
                                Sprite = reader.CheckObject<byte[]>("sprite")
                            };
                            pokemons.Add(p);
                        }
                    }
                } // Command
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
