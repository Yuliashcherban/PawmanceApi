namespace Pawmence.API.Entities;

public class Like
{
    public int FromProfileId { get; set; }
    public int ToProfileId   { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}