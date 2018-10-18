namespace PokemonStats_WPF.Models {
    public class Pokemon {
        public int IndexNumber { get; set; }
        public string Name { get; set; }
        public string Genus { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public string Ability1 { get; set; }
        public string Ability2 { get; set; }
        public string HiddenAbility { get; set; }
        public byte[] Type1_Image { get; set; }
        public byte[] Type2_Image { get; set; }
        public Stats Stats { get; set; }
        public Form Forms { get; set; }
        public string EggGroups { get; set; }
        public string Color { get; set; }
        public string Shape { get; set; }
        public byte[] Shape_Image { get; set; }
        public string Habitat { get; set; }
        public byte[] Habitat_Image { get; set; }
        public byte[] Footprint { get; set; }
        public int GenderRate { get; set; }
        public int CaptureRate { get; set; }
        public int BaseHappiness { get; set; }
        public int HatchCounter { get; set; }
        public long HatchSteps{ get; set; }
        public string GrowthRate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BaseExperience { get; set; }
        public string SpeciesSummary { get; set; }
        public byte[] Icon { get; set; }
        public byte[] Sprite { get; set; }
        public string Details {
            get {
                return string.Format("{0} is a {1} Pokemon.", Name, ((Ability2 != null) ? (string.Join("/", new string[] { Ability1, Ability2 })) : Ability1));
            }
        }
    }
}
