// CreateServiceDto.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CreateServiceDto
{
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

    [Required]
    [JsonPropertyName("storeId")]
    public string StoreId { get; set; } = string.Empty;
}
