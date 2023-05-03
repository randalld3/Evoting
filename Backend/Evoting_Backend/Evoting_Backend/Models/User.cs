namespace Evoting_Backend.Models;

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