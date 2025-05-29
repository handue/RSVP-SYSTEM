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
    public StoreHour? StoreHour { get; set; }
    // public List<StoreHour> StoreHours { get; set; } = new List<StoreHour>();
    // old: public List<Service> Services { get; set; } = new List<T>();
    // new: public List<Service> Services { get; set; } = [];
    public List<StoreService> StoreServices { get; set; } = [];


    public List<Reservation> Reservations { get; set; } = [];
}