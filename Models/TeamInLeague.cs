using System.Text.Json.Serialization;

namespace DK.EFootballClub.PlayerDataUsvc;

public class TeamInLeague
{
    [JsonPropertyName("teamName")]
    public required string TeamName { get; set; }
    [JsonPropertyName("matchesPlayed")]
    public int MatchesPlayed { get; set; }
    [JsonPropertyName("points")]
    public int Points { get; set; }
}