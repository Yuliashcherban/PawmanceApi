using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Pawmence.API.DTOs
{
    public class CreateProfileRequestDto
    {
        [Required]
        public string Photo { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        public string Nickname { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Breed { get; set; }

        [Required]
        public string Location { get; set; }

        [Range(1, 25)]
        public int Age { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public string Description { get; set; }
    }

    public class LocationDto
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }
    }
}