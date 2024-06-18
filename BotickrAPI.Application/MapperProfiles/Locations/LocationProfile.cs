using AutoMapper;
using BotickrAPI.Application.Dtos.Locations;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Application.MapperProfiles.Locations
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationEntity, LocationDto>()
                .ForMember(dest => dest.LocationId, method => method.MapFrom(src => src.Id))
                .ForMember(dest => dest.City, method => method.MapFrom(src => src.City))
                .ForMember(dest => dest.Venue, method => method.MapFrom(src => src.Venue))
                .ForMember(dest => dest.Capacity, method => method.MapFrom(src => src.Capacity));
        }
    }
}
