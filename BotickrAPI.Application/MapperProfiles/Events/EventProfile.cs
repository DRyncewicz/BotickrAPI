using AutoMapper;
using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Application.MapperProfiles.Events;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<NewEventDto, EventEntity>().ReverseMap();
    }
}
