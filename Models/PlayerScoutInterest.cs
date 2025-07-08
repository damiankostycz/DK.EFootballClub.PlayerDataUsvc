namespace DK.EFootballClub.PlayerDataUsvc;

public class PlayerScoutInterest
{
    public required Player Player { get; set; }
    public List<Scout> InterestedScouts { get; set; } = new List<Scout>();
}