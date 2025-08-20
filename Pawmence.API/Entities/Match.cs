namespace Pawmence.API.Entities;

public class Match
{
    public int Id { get; set; }
    public int ProfileA { get; set; }
    public int ProfileB { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int LeastId { get; private set; }
    public int GreatestId { get; private set; }
}