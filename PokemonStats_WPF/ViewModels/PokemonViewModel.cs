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

namespace PokemonStats_WPF {
    public class PokemonViewModel : DataWorker {
        public List<Pokemon> RetrieveAllPokemon() {
            List<Pokemon> pokemons = new List<Pokemon>();
            using (IDbConnection connection = database.CreateOpenConnection()) {
                using (IDbCommand command = database.CreateCommand()) {
                    command.Connection = connection;
                    command.CommandText = Query.GetAllPokemon;
                    command.Prepare();
                    using (IDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            Pokemon p = new Pokemon{
                                IndexNumber = reader.CheckValue<int>("pokemon_id"),
                                Name = reader.CheckObject<string>("name"),
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
                                Ability1 = reader.CheckObject<string>("ability1"),
                                Ability2 = reader.CheckObject<string>("ability2"),
                                HiddenAbility = reader.CheckObject<string>("hidden_ability")
                            };
                            pokemons.Add(p);
                        }
                    }
                } // Command
            }
            return pokemons;
        }
    }
}
