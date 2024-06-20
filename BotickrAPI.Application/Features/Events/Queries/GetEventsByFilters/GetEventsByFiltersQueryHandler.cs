using AutoMapper;
using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Domain.Repositories;
using MediatR;

namespace BotickrAPI.Application.Features.Events.Queries.GetEventsByFilters;

public class GetEventsByFiltersQueryHandler(IMapper _mapper, IEventRepository _eventRepository) : IRequestHandler<GetEventsByFiltersQuery, IEnumerable<EventDto>>
{
    private readonly IEventRepository _eventRepository = _eventRepository;

    private readonly IMapper _mapper = _mapper;

    public async Task<IEnumerable<EventDto>> Handle(GetEventsByFiltersQuery request, CancellationToken cancellationToken)
    {
        var filteredEvents = await _eventRepository.GetByFiltersAsync(request.SearchString, request.EventStart, request.LocationId, cancellationToken);

        if(request.PageSize != 0 && request.PageIndex != 0)
        {
            filteredEvents = filteredEvents.Skip((request.PageIndex -1) * request.PageSize).Take(request.PageSize);  
        }

        var result = _mapper.Map<IEnumerable<EventDto>>(filteredEvents);

        return result;
    }
}
