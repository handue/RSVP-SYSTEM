
using System.Text.Json.Serialization;

public class StoreHourDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("storeId")]
    public string StoreId { get; set; } = string.Empty;

    [JsonPropertyName("regularHours")]
    public List<RegularHourDto> RegularHours { get; set; } = [];

    [JsonPropertyName("specialDate")]
    public List<SpecialDateDto>? SpecialDate { get; set; }
}
