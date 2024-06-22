using AutoMapper;
using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Application.MapperProfiles.Events;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<NewEventDto, EventEntity>()
                  .ForMember(dest => dest.OrganizerId, opt => opt.Ignore())
                  .ForMember(dest => dest.Status, opt => opt.Ignore())
                  .ForMember(dest => dest.Location, opt => opt.Ignore())
                  .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                  .ForMember(dest => dest.Tickets, opt => opt.Ignore())
                  .ForMember(dest => dest.EventArtists, opt => opt.Ignore())
                  .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                  .ForMember(dest => dest.Id, opt => opt.Ignore())
                  .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                  .ForMember(dest => dest.Created, opt => opt.Ignore())
                  .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                  .ForMember(dest => dest.Modified, opt => opt.Ignore())
                  .ForMember(dest => dest.StatusId, opt => opt.Ignore())
                  .ForMember(dest => dest.InactivatedBy, opt => opt.Ignore())
                  .ForMember(dest => dest.Inactivated, opt => opt.Ignore());

        CreateMap<EventEntity, EventDto>()
            .ForMember(dest => dest.Artists, method => method.MapFrom(src => src.EventArtists.Select(p => p.Artist)));

        CreateMap<EventEntity, DetailEventInfoDto>()
            .ForMember(dest => dest.EventType, method => method.MapFrom(src => src.EventType))
            .ForMember(dest => dest.Duration, method => method.MapFrom(src => src.Duration))
            .ForMember(dest => dest.Description, method => method.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, method => method.MapFrom(src => src.Status))
            .ForMember(dest => dest.StartTime, method => method.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.LocationId, method => method.MapFrom(src => src.LocationId))
            .ForMember(dest => dest.Id, method => method.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, method => method.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImagePath, method => method.MapFrom(src => src.ImagePath))
            .ForMember(dest => dest.Tickets, method => method.Ignore());


    }
}
