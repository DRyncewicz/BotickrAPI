using AutoMapper;
using BotickrAPI.Application.Dtos.Artists;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Application.MapperProfiles.Artists;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        CreateMap<ArtistDto, ArtistEntity>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
            .ForMember(dest => dest.Tickets, opt => opt.Ignore())
            .ForMember(dest => dest.EventArtists, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Modified, opt => opt.Ignore())
            .ForMember(dest => dest.StatusId, opt => opt.Ignore())
            .ForMember(dest => dest.InactivatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Inactivated, opt => opt.Ignore())
            .ReverseMap();
    }
}
