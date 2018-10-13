using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonStats_WPF.Models {
    public class Pokemon {
        [DisplayName("No")]
        public int IndexNumber { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public string Shape { get; set; }
        public string Habitat { get; set; }

        [DisplayName("Gender rate")]
        public int GenderRate { get; set; }

        [DisplayName("Capture rate")]
        public int CaptureRate { get; set; }

        [DisplayName("Base happiness rate")]
        public int BaseHappiness { get; set; }

        [DisplayName("Growth rate")]
        public string GrowthRate { get; set; }

        public int Height { get; set; }
        public int Weight { get; set; }

        [DisplayName("Base experience")]
        public int BaseExperience { get; set; }

        [DisplayName("Ability 1")]
        public string Ability1 { get; set; }

        [DisplayName("Ability 2")]
        public string Ability2 { get; set; }

        [DisplayName("Hidden ability")]
        public string HiddenAbility { get; set; }
    }
}
