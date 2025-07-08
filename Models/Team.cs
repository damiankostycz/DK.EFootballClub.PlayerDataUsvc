using System.Text.Json.Serialization;

namespace DK.EFootballClub.PlayerDataUsvc;

public class Team
{
    [JsonPropertyName("teamId")]
    public string TeamId { get; set; } = string.Empty;
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("league")]
    public List<TeamInLeague>? League { get; set; }
}