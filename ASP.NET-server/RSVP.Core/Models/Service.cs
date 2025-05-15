using System.ComponentModel.DataAnnotations;

namespace RSVP.Core.Models;

public class Service

{

    [Required]
    [JsonPropertyName("id")]
    [Key]
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("serviceId")]
    [StringLength(100)]
    public String ServiceId { get; set; }



    [Required]
    [JsonPropertyName("name")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [Required]
    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    // Navigation properties
    // 외래 키 속성 (Foreign Key Property)
    [Required]
    [JsonPropertyName("store_id")]
    public int StoreId { get; set; }

    // 탐색 속성 (Navigation Property)
    // 참고: 이 속성은 Entity Framework Core에서 관계형 데이터베이스의 관계를 표현하기 위해 사용됨
    // Note: This property is used in Entity Framework Core to represent relationships in relational databases
    // 
    // 클라이언트(service.ts)에는 storeId가 없지만 백엔드에서는 관계형 데이터베이스 구조를 위해 필요함
    // While storeId doesn't exist in client (service.ts), it's necessary in the backend for relational database structure
    //
    // 외래 키 속성을 명시적으로 추가하는 것이 권장됨
    // It's recommended to explicitly add foreign key properties
    public Store Store { get; set; } = null!;

    // 탐색 속성: 서비스와 관련된 모든 예약 목록
    // Navigation property: List of all reservations related to this service
    //
    // ICollection<T>: 컬렉션 데이터 구조를 나타내는 인터페이스로, 여기서는 일대다(one-to-many) 관계를 표현
    // ICollection<T>: Interface representing collection data structure, used here to express one-to-many relationship
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}