using AutoMapper;
using BotickrAPI.Application.Dtos.Artists;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Application.MapperProfiles.Artists;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        CreateMap<ArtistDto, ArtistEntity>()
            .ReverseMap();
    }
}
