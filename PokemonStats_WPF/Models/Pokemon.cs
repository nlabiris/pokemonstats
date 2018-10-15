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

        [DisplayName("Type 1")]
        public string Type1 { get; set; }

        [DisplayName("Type 2")]
        public string Type2 { get; set; }

        [DisplayName("Ability 1")]
        public string Ability1 { get; set; }

        [DisplayName("Ability 2")]
        public string Ability2 { get; set; }

        [DisplayName("Hidden ability")]
        public string HiddenAbility { get; set; }

        public byte[] Type1_Image { get; set; }

        public byte[] Type2_Image { get; set; }

        public int HP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int SpAtk { get; set; }
        public int SpDef { get; set; }
        public int Spe { get; set; }

        public string Color { get; set; }
        public string Shape { get; set; }
        public string Habitat { get; set; }

        [DisplayName("Gender rate")]
        public int GenderRate { get; set; }

        [DisplayName("Capture rate")]
        public int CaptureRate { get; set; }

        [DisplayName("Base happiness")]
        public int BaseHappiness { get; set; }

        [DisplayName("Growth rate")]
        public string GrowthRate { get; set; }

        public int Height { get; set; }
        public int Weight { get; set; }

        [DisplayName("Base experience")]
        public int BaseExperience { get; set; }

        public string SpeciesSummary { get; set; }

        public byte[] Icon { get; set; }

        public byte[] Sprite { get; set; }

        public string Details {
            get {
                return String.Format("{0} is a {1} Pokemon.", Name, ((Ability2 != null) ? (string.Join("/", new string[] { Ability1, Ability2 })) : Ability1));
            }
        }
    }
}
