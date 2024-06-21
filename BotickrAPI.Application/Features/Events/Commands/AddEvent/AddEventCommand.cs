using BotickrAPI.Application.Dtos.Events;
using MediatR;

namespace BotickrAPI.Application.Features.Events.Commands.AddEvent;

public class AddEventCommand : IRequest<int>
{
    public NewEventDto NewEvent { get; set; } = new NewEventDto();
}
