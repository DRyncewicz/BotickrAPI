using BotickrAPI.Application.Dtos.Events;
using MediatR;

namespace BotickrAPI.Application.Features.Events.Commands;

public class AddEventCommand : IRequest<int>
{
    public NewEventDto NewEvent { get; set; } = new NewEventDto();
}
