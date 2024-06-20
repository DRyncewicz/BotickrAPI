using BotickrAPI.Application.Dtos.Locations;
using BotickrAPI.Application.Features.Events.Commands;
using BotickrAPI.Controller.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BotickrAPI.Controller;

public class EventController(IMediator _mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = _mediator;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<int>> AddAsync(AddEventCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        return Created(string.Empty, result);
    }
}
