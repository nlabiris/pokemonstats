namespace PokemonStats_WPF.Models {
    public class Evolution {
        public string Name { get; set; }
        public byte[] Icon { get; set; }
        public int? EvolvesFromSpeciesId { get; set; }
        public int Stage { get; set; }
        public string EvolutionTrigger { get; set; }
        public string TriggerItem { get; set; }
        public int MinimumLevel { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public string KnownMove { get; set; }
        public string KnownMoveType { get; set; }
        public string HeldItem { get; set; }
        public bool IsBaby { get; set; }
        public string TimeOfDay { get; set; }
        public int MinimumHappiness { get; set; }
        public int MinimumAffection { get; set; }
        public int MinimumBeauty { get; set; }
        public int? RelativePhysicalStats { get; set; }
        public string PartySpecies { get; set; }
        public string PartyType { get; set; }
        public string TradeSpecies { get; set; }
        public bool NeedsOverworldRain { get; set; }
        public bool TurnUpsideDown { get; set; }
    }
}
