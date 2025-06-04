using System.Text.Json.Serialization;

public class RegularHourDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("storeHourId")]
    public int StoreHourId { get; set; }

    [JsonPropertyName("day")]
    public int Day { get; set; }  //    Change : DayOfWeek → int

    [JsonPropertyName("open")]
    public string Open { get; set; } = "09:00";  //    Change : TimeSpan → string으로 변경

    [JsonPropertyName("close")]
    public string Close { get; set; } = "18:00";  //    Change : TimeSpan → string

    [JsonPropertyName("isClosed")]
    public bool IsClosed { get; set; }
}