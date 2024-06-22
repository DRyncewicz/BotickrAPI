using AutoMapper;
using BotickrAPI.Application.Dtos.Tickets;
using BotickrAPI.Domain.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace BotickrAPI.Application.MapperProfiles.Tickets;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<NewTicketDto, TicketEntity>()
            .ForMember(dest => dest.Price, method => method.MapFrom(src => src.Price))
            .ForMember(dest => dest.Quantity, method => method.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.TicketType, method => method.MapFrom(src => src.TicketType))
            .ForMember(dest => dest.Event, method => method.Ignore())
            .ForMember(dest => dest.StatusId, method => method.Ignore())
            .ForMember(dest => dest.Created, method => method.Ignore())
            .ForMember(dest => dest.BookingDetails, method => method.Ignore())
            .ForMember(dest => dest.Modified, method => method.Ignore())
            .ForMember(dest => dest.ModifiedBy, method => method.Ignore())
            .ForMember(dest => dest.Inactivated, method => method.Ignore())
            .ForMember(dest => dest.InactivatedBy, method => method.Ignore())
            .ForMember(dest => dest.EventId, method => method.Ignore())
            .ForMember(dest => dest.Id, method => method.Ignore())
            .ForMember(dest => dest.CreatedBy, method => method.Ignore())
            .ReverseMap();

        CreateMap<TicketEntity, TicketDto>()
            .ForMember(dest => dest.TicketType, method => method.MapFrom(src => src.TicketType))
            .ForMember(dest => dest.TotalQuantity, method => method.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, method => method.MapFrom(src => src.Price))
                .ForMember(dest => dest.AvailableQuantity, method => method.MapFrom(src =>
                    src.BookingDetails == null || !src.BookingDetails.Any()
                    ? src.Quantity
                    : src.Quantity - src.BookingDetails.Sum(x => x.Quantity)))
                .ForMember(dest => dest.IsSoldOut, method => method.MapFrom(src =>
                    src.BookingDetails != null && src.BookingDetails.Any()
                    ? src.Quantity == src.BookingDetails.Sum(x => x.Quantity)
                    : false));

    }
}
