using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CalendarEventResult
{
    [Required]
    [JsonPropertyName("eventId")]
    public string EventId { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("htmlLink")]
    public string HtmlLink { get; set; } = string.Empty;
}