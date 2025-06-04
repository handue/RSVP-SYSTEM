
using System.Text.Json.Serialization;

public class SpecialDateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;  // Change : DateTime → string

    [JsonPropertyName("open")]
    public string Open { get; set; } = "09:00";  // Change : TimeSpan → string

    [JsonPropertyName("close")]
    public string Close { get; set; } = "18:00";  // Change : TimeSpan → string

    [JsonPropertyName("isClosed")]
    public bool IsClosed { get; set; }
}