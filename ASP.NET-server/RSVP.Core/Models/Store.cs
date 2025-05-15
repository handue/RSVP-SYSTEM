using System.ComponentModel.DataAnnotations;

namespace RSVP.Core.Models;

public class Store
{
    [Required]
    [JsonPropertyName("id")]
    [Key]
    public int Id { get; set; }

    

    [Required]
    [JsonPropertyName("name")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;



    [EmailAddress]
    [StringLength(100)]
    [JsonPropertyName("storeEmail")]
    public string? Email { get; set; }

    // Navigation properties
    public ICollection<StoreHour> StoreHours { get; set; } = new List<StoreHour>();
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}