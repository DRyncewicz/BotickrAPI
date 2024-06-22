using BotickrAPI.Application.Dtos.Events;
using MediatR;

namespace BotickrAPI.Application.Features.Events.Queries.GetEventDetailsById;

public class GetEventDetailsByIdQuery : IRequest<DetailEventInfoDto>
{
    public int Id { get; set; }
}
