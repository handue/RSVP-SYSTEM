using AutoMapper;
using RSVP.Core.DTOs;
using RSVP.Core.Models;

namespace RSVP.Core.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateReservationDto -> Reservation
        CreateMap<CreateReservationDto, Reservation>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ReservationStatus.Pending));
        // * ForMember is a method that allows us to customize the mapping process.
        // * dest = Reservation, src = CreateReservationDto, opt = the tool set for the mapping
        // * there's no status in dto, so when we create a reservation, we need to set the status to pending. ForMember 

        // Reservation -> ReservationResponseDto
        CreateMap<Reservation, ReservationResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString().ToLower()));
        // why we converse Status to string and to lower
        //  * enum default automapping is just used with ToString,
        // * in general service, frontend want to get a value as a lowercase. that's why we do this. 

        CreateMap<CreateServiceDto, Service>();
        CreateMap<Service, ServiceResponseDto>();

        CreateMap<CreateStoreDto, Store>();
        CreateMap<Store, StoreResponseDto>();
    }
}