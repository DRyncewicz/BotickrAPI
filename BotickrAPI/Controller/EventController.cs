using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Application.Dtos.Locations;
using BotickrAPI.Application.Features.Events.Commands.AddEvent;
using BotickrAPI.Application.Features.Events.Queries.GetEventsByFilters;
using BotickrAPI.Controller.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BotickrAPI.Controller;
/// <summary>
/// Controller handling HTTP request related with Events
/// </summary>
/// <param name="_mediator"></param>
public class EventController(IMediator _mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = _mediator;

    /// <summary>
    /// Returns new event ID on succes call, returns ProblemDetails model when error occurs
    /// </summary>
    /// <param name="command"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<int>> AddAsync(AddEventCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        return Created(string.Empty, result);
    }

    [HttpGet("GetByFilters")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EventDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetByFilters([FromQuery] GetEventsByFiltersQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);

        return Ok(result);
    }
}
