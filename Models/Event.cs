﻿using System.Text.Json.Serialization;

namespace DK.EFootballClub.PlayerDataUsvc
{
    public class Event
    {
        [JsonPropertyName("minute")]
        public int Minute { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("player")]
        public string Player { get; set; } = string.Empty;

        [JsonPropertyName("team")]
        public string Team { get; set; } = string.Empty;
    }
}
