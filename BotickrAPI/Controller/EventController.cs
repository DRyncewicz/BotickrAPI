using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Application.Dtos.Locations;
using BotickrAPI.Application.Features.Events.Commands.AddEvent;
using BotickrAPI.Application.Features.Events.Queries.GetEventDetailsById;
using BotickrAPI.Application.Features.Events.Queries.GetEventsByFilters;
using BotickrAPI.Controller.Base;
using BotickrAPI.Domain.Exceptions;
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
    /// Returns a new event ID in case of a successful call, returns the ProblemDetails model in case of an error.
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

    /// <summary>
    /// Returns a list of EventDto according to the filters specified in Query, returns the ProblemDetails model in case of an error.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("GetByFilters")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EventDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetByFilters([FromQuery] GetEventsByFiltersQuery query, CancellationToken ct)
    {
        var result = await _mediator.Send(query, ct);

        return Ok(result);
    }

    /// <summary>
    /// Returns DetailtEventInfoDto for specific EventId provided by query, returns the ProblemsDetails model in case of an error.
    /// </summary>
    /// <param name="EventId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("{EventId}/GetDetails")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailEventInfoDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<DetailEventInfoDto>> GetDetailsById(int EventId, CancellationToken ct)
    {

        var query = new GetEventDetailsByIdQuery()
        {
            Id = EventId
        };
        try
        {
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            var notFound = new ProblemDetails()
            {
                Type = nameof(NotFoundException),
                Detail = ex.Message,
            };

            return NotFound(notFound);
        }
    }
}
