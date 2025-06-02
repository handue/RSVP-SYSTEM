using System.Text.Json.Serialization;
using RSVP.Core.Models;

public class StoreResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("storeId")]
    public string StoreId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;

    [JsonPropertyName("storeEmail")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("storeHour")]
    public StoreHour? StoreHour { get; set; }
}