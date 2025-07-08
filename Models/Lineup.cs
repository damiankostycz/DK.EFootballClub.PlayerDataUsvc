using System.Text.Json.Serialization;

namespace DK.EFootballClub.PlayerDataUsvc
{
    public class Lineup
    {
        [JsonPropertyName("formation")]
        public string? Formation { get; set; }

        [JsonPropertyName("goalkeeper")]
        public int Goalkeeper { get; set; }

        [JsonPropertyName("defenders")]
        public List<int>? Defenders { get; set; }

        [JsonPropertyName("midfielders")]
        public List<int>? Midfielders { get; set; }

        [JsonPropertyName("strikers")]
        public List<int>? Strikers { get; set; }

        [JsonPropertyName("substitutes")]
        public List<int>? Substitutes { get; set; }
    }
}
