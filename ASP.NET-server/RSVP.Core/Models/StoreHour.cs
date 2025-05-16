using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace RSVP.Core.Models;

public class StoreHour
{
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("storeId")]
    [Required]
    public string StoreId { get; set; } = string.Empty;

    [JsonPropertyName("regularHours")]
    [Required]
    public List<RegularHour> RegularHours { get; set; } = new List<RegularHour>();

    [JsonPropertyName("specialDate")]
    public List<SpecialDate>? SpecialDate { get; set; }

    // Navigation property
    public Store Store { get; set; } = null!;
}

public class RegularHour
{

    [JsonPropertyName("day")]
    [Required]
    public string Day { get; set; } = string.Empty;


    [JsonPropertyName("open")]
    [Required]
    public TimeSpan Open { get; set; } = string.Empty;

    [JsonPropertyName("close")]
    [Required]
    public TimeSpan Close { get; set; } = string.Empty;

    [JsonPropertyName("isClosed")]
    [Required]
    public bool IsClosed { get; set; }
}

public class SpecialDate
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("open")]
    public TimeSpan Open { get; set; } = string.Empty;

    [JsonPropertyName("close")]
    public TimeSpan Close { get; set; } = string.Empty;

    [JsonPropertyName("isClosed")]
    public bool IsClosed { get; set; }
}