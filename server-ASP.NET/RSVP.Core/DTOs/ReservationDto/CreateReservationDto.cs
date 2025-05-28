// CreateReservationDto.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CreateReservationDto
{
    [Required]
    [JsonPropertyName("store_id")]
    public string StoreId { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("service_id")]
    public string ServiceId { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [JsonPropertyName("name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(20)]
    [JsonPropertyName("phone")]
    public string CustomerPhone { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    [JsonPropertyName("email")]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("reservation_date")]
    public DateTime ReservationDate { get; set; }

    [Required]
    [JsonPropertyName("reservation_time")]
    public TimeSpan ReservationTime { get; set; }

    [JsonPropertyName("comments")]
    public string? Notes { get; set; }

    [Required]
    [JsonPropertyName("agreed_to_terms")]
    public bool AgreedToTerms { get; set; }
}



public class UpdateReservationDto : CreateReservationDto
{
    public int Id { get; set; }
}