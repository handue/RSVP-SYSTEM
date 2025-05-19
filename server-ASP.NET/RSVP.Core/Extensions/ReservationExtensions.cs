using RSVP.Core.DTOs;
using RSVP.Core.Models;

namespace RSVP.Core.Extensions;

public static class ReservationExtensions
{
    // 복잡한 변환이 필요한 경우 사용할 확장 메서드
    // 허나 기본적으로는 AutoMapper 사용할거야.

    // Extension Method when complex conversion is needed
    // However, I will use AutoMapper for most conversions
    public static ReservationResponseDto ToDetailedDto(this Reservation model)
    {
        return new ReservationResponseDto
        {
            Id = model.Id,
            StoreId = model.StoreId,
            ServiceId = model.ServiceId,
            CustomerName = model.CustomerName,
            CustomerPhone = model.CustomerPhone,
            CustomerEmail = model.CustomerEmail,
            ReservationDate = model.ReservationDate,
            ReservationTime = model.ReservationTime,
            Status = model.Status.ToString().ToLower(),
            Notes = model.Notes,
            AgreedToTerms = model.AgreedToTerms
        };
    }
} 