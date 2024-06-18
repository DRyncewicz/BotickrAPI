using BotickrAPI.Application.Dtos.Locations;
using BotickrAPI.Application.Features.Locations.Queries;
using BotickrAPI.Controller.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BotickrAPI.Controller;
/// <summary>
/// Location controller handling location related operations
/// </summary>
/// <param name="_mediator"></param>
public class LocationController(IMediator _mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = _mediator;

    /// <summary>
    /// Returns all locations for events registred in app
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LocationDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll(CancellationToken ct)
    {
        var query = new GetAllLocationsQuery();

        var data = await _mediator.Send(query, ct);

        return Ok(data);
    }
}