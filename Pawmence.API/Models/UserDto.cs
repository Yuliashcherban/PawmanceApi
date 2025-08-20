using System.ComponentModel.DataAnnotations;

namespace Pawmence.API.Models
{
    public class UserDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^\S+$", ErrorMessage = "No spaces allowed in Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one letter and one number")]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
