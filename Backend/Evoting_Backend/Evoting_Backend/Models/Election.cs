namespace Evoting_Backend.Models;

public class Election
{
    public int ElectionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? PoliticalParty { get; set; }
    public string? Location { get; set; }
}