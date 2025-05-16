using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace RSVP.Core.Models;

public class Store
{
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("storeId")]
    public string StoreId { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    [JsonPropertyName("storeEmail")]
    public string Email { get; set; } = string.Empty;

    // Navigation properties
    public List<StoreHour> StoreHours { get; set; } = new List<StoreHour>();
    public List<Service> Services { get; set; } = new List<Service>();
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}