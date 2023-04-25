namespace Evoting_Backend.Models;

public class Election
{
    public int ElectionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? PoliticalParty { get; set; }
    public string? Location { get; set; }
}

public class User
{
    public int VoterId { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int? GovernmentId { get; set; }
    public string RecoveryQuestion { get; set; }
    public string RecoveryAnswer { get; set; }
}

public class Vote
{
    public int VoterId { get; set; }
    public int? Id1 { get; set; }
    public string? Id1Vote { get; set; }
    public int? Id2 { get; set; }
    public string? Id2Vote { get; set; }
    public int? Id3 { get; set; }
    public string? Id3Vote { get; set; }
    public int? Id4 { get; set; }
    public string? Id4Vote { get; set; }
    public int? Id5 { get; set; }
    public string? Id5Vote { get; set; }
}