using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CreateStoreDto
{
    [Required]
    [JsonPropertyName("storeId")]
    public string StoreId { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    [JsonPropertyName("storeEmail")]
    public string Email { get; set; } = string.Empty;
}

public class UpdateStoreDto : CreateStoreDto
{

    [JsonPropertyName("Id")]
    public string Id { get; set; } = string.Empty;
}