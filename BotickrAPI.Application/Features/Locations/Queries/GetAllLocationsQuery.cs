using BotickrAPI.Application.Dtos.Locations;
using MediatR;

namespace BotickrAPI.Application.Features.Locations.Queries;

public class GetAllLocationsQuery : IRequest<IEnumerable<LocationDto>>
{
}
