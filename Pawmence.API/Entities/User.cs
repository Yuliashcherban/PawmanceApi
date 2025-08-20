namespace Pawmence.API.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public bool IsActive { get; set; } = false;
    public List<DogProfile> DogProfiles { get; set; } = new();
  
    
    public void UpdateIsActive()
    {
        IsActive = DogProfiles.Any();
    }

}