namespace PokemonStats_WPF.Models {
    public class Form {
        public string FormName { get; set; }
        public string PokemonName { get; set; }
        public int IsDefault { get; set; }
        public int IsBattleOnly { get; set; }
        public int IsMega { get; set; }
        public byte[] Icon { get; set; }
        public byte[] Sprite { get; set; }
    }
}
