﻿using System.Text.Json.Serialization;

namespace DK.EFootballClub.PlayerDataUsvc
{
    public class Match
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("events")]
        public List<Event>? Events { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("homeTeam")]
        public string? HomeTeam { get; set; }

        [JsonPropertyName("awayTeam")]
        public string? AwayTeam { get; set; }

        [JsonPropertyName("lineupTeam1")]
        public Lineup? LineupTeam1 { get; set; }

        [JsonPropertyName("lineupTeam2")]
        public Lineup? LineupTeam2 { get; set; }

        [JsonPropertyName("result")]
        public string? Result { get; set; }
    }
}
