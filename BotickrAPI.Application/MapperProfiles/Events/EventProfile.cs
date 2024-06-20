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
    }
}
