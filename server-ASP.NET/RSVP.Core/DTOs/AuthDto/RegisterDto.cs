using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RSVP.Core.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
    }
} 