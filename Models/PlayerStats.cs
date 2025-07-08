using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace DK.EFootballClub.PlayerDataUsvc;

public class PlayerStats
{
    [BsonElement("matches")]
    public int Matches { get; set; }

    [BsonElement("minutes")]
    public int Minutes { get; set; }

    [BsonElement("goals")]
    public int Goals { get; set; }

    [BsonElement("assists")]
    public int Assists { get; set; }

    [BsonElement("red_cards")]
    public int RedCards { get; set; }

    [BsonElement("yellow_cards")]
    public int YellowCards { get; set; }

    [BsonElement("clean_sheets")]
    public int CleanSheets { get; set; }
}