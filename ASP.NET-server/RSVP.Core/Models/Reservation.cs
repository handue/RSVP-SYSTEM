using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RSVP.Core.Models;

public class Reservation
{
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("store_Id")]
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
    // automatic validation for Phone-Number
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
    // TimeSpan 사용 이유: 시간 데이터 검증, 시간 계산 용이성, 타입 안전성
    // Benefits of TimeSpan: time data validation, easy time calculations, type safety
    // 클라이언트에서 string으로 보내도 서버에서 자동 변환됨
    // Server automatically converts from string sent by client
    public TimeSpan ReservationTime { get; set; }

    // [Required]
    // [JsonPropertyName("start_time")]
    // public TimeSpan StartTime { get; set; }

    // [Required]
    // [JsonPropertyName("end_time")]
    // public TimeSpan EndTime { get; set; }

    [Required]
    [JsonPropertyName("status")]
    public ReservationStatus Status { get; set; }

    [JsonPropertyName("comments")]
    public string? Notes { get; set; }

    [Required]
    [JsonPropertyName("agreed_to_terms")]
    public bool AgreedToTerms { get; set; }

    // Navigation properties
    public Store Store { get; set; } = null!;
    public Service Service { get; set; } = null!;
}

public enum ReservationStatus
{
    [JsonPropertyName("pending")]
    Pending,
    [JsonPropertyName("confirmed")]
    Confirmed,
    [JsonPropertyName("cancelled")]
    Cancelled
}