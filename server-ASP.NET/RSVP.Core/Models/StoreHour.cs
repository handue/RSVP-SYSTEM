using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;


namespace RSVP.Core.Models;

public class StoreHour
{
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("storeId")]
    [Required]
    public string StoreId { get; set; } = string.Empty;

    [JsonPropertyName("regularHours")]
    [Required]
    public List<RegularHour> RegularHours { get; set; } = new List<RegularHour>();

    [JsonPropertyName("specialDate")]
    public List<SpecialDate>? SpecialDate { get; set; }

    // Navigation property
    [JsonIgnore]
    public Store Store { get; set; } = null!;
}

public class RegularHour
{
    // 명시적 ID (데이터베이스에서만 사용됨)
    public int Id { get; set; }

    // StoreHour와의 관계를 위한 외래 키
    public int StoreHourId { get; set; }

    [JsonPropertyName("day")]
    [Required]
//    * default value sunday (0) is set. should change the value when we put the default value into DB (in Data folder, ApplicationDbContext)
// * but I set up it as one-one relation. so we don't need to modify. 
    public DayOfWeek Day { get; set; }

    [JsonPropertyName("open")]
    [Required]
    public TimeSpan Open { get; set; } = new TimeSpan(9, 0, 0);

    [JsonPropertyName("close")]
    [Required]
    public TimeSpan Close { get; set; } = new TimeSpan(18, 0, 0);

    [JsonPropertyName("isClosed")]
    [Required]
    public bool IsClosed { get; set; }
}


public class SpecialDate
{

      // 명시적 ID (데이터베이스에서만 사용됨)
    public int Id { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("open")]
    public TimeSpan Open { get; set; }

    [JsonPropertyName("close")]
    public TimeSpan Close { get; set; }

    [JsonPropertyName("isClosed")]
    public bool IsClosed { get; set; }
}