using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonStats_WPF.DataAccess;

namespace PokemonStats_WPF.Models {
    public class Data {
        private List<Pokemon> pokemons = null;
        
        public List<Pokemon> Pokemons {
            get { return pokemons; }
        }

        public Data() {
            
        }
    }
}
