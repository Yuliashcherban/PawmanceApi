namespace Pawmence.API.Entities;

public class Chat
{
    public int Id { get; set; } 
    public int MatchId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
