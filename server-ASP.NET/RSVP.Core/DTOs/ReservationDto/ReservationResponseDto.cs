// ReservationResponseDto.cs
using System.Text.Json.Serialization;

public class ReservationResponseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("store_id")]
    public string StoreId { get; set; } = string.Empty;

    [JsonPropertyName("service_id")]
    public string ServiceId { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string CustomerName { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string CustomerPhone { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string CustomerEmail { get; set; } = string.Empty;

    [JsonPropertyName("reservation_date")]
    public DateTime ReservationDate { get; set; }

    [JsonPropertyName("reservation_time")]
    public TimeSpan ReservationTime { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("comments")]
    public string? Notes { get; set; }

    [JsonPropertyName("agreedToTerms")]
    public bool AgreedToTerms { get; set; }
}