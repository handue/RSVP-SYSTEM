using System.Text.Json.Serialization;

public class ServiceResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("serviceId")]
    public string ServiceId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("storeId")]
    public string? StoreId { get; set; }
}