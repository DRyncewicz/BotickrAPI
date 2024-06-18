using AutoMapper;
using BotickrAPI.Application.Dtos.Locations;
using BotickrAPI.Domain.Repositories;
using MediatR;

namespace BotickrAPI.Application.Features.Locations.Queries;

public class GetAllLocationsQueryHandler(ILocationRepository _locationRepository, IMapper _mapper) : IRequestHandler<GetAllLocationsQuery, IEnumerable<LocationDto>>
{
    private readonly ILocationRepository _locationRepository = _locationRepository;

    private readonly IMapper _mapper = _mapper;

    public async Task<IEnumerable<LocationDto>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        var locationEntities = await _locationRepository.GetAllAsync();

        var locationDtos = _mapper.Map<IEnumerable<LocationDto>>(locationEntities);

        return locationDtos;
    }
}
