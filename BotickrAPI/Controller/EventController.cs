using BotickrAPI.Application.Features.Events.Commands;
using BotickrAPI.Controller.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BotickrAPI.Controller;

public class EventController(IMediator _mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = _mediator;

    [HttpPost]
    public async Task<ActionResult<int>> AddAsync(AddEventCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        return Created(string.Empty, result);
    }
}
