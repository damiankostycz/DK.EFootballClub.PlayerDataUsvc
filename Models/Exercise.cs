﻿using System.Text.Json.Serialization;

namespace DK.EFootballClub.PlayerDataUsvc;

public class Exercise
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    [JsonPropertyName("reps")]
    public string Reps { get; set; } = string.Empty;
}