namespace PokemonStats_WPF.Models {
    public class Form {
        public string FormName { get; set; }
        public string PokemonName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsBattleOnly { get; set; }
        public bool IsMega { get; set; }
        public byte[] Icon { get; set; }
        public byte[] Sprite { get; set; }
    }
}
