using System.ComponentModel.DataAnnotations;
using RSVP.Core.Models;

namespace RSVP.Core.Models;

public class StoreService
{
    [Required]
    public string StoreId { get; set; } = string.Empty;


    [Required]
    public string ServiceId { get; set; } = string.Empty;


    // Navigation properties
    public Store Store { get; set; } = null!;

    public Service Service { get; set; } = null!;
}