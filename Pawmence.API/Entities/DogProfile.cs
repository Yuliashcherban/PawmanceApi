using System.Text.Json.Serialization;

namespace Pawmence.API.Entities;
public class DogProfile
{
    public int Id { get; set; }

    public string Nickname { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;

    public int UserId { get; set; }   
    [JsonIgnore]
    public User User { get; set; } = default!;

    public bool IsActive { get; set; } = true;
}
