using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace RSVP.Core.Models;

public class Service
{
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [JsonPropertyName("serviceId")]
    public string ServiceId { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [Required]
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    // * decimal = It's used for currency (money)
    // * because it provides precise calculation with fractional/decimal values
    // * and avoids rounding errors common in floating-point arithmetic
    public string? StoreId { get; set; }

    // [Required]
    // [JsonPropertyName("serviceId")]
    // public string ServiceId { get; set; } = string.Empty;

    // Navigation properties
    public Store Store { get; set; } = null!;
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}