using BotickrAPI.Application.Dtos.Events;
using MediatR;

namespace BotickrAPI.Application.Features.Events.Commands;

public class AddEventCommand : IRequest<int>
{
    public EventDto NewEvent { get; set; } = new EventDto();
}
