using AutoMapper;
using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;
using MediatR;

namespace BotickrAPI.Application.Features.Events.Commands;

public class AddEventCommandHandler(IEventRepository _eventRepository, IMapper _mapper) : IRequestHandler<AddEventCommand, int>
{
    private readonly IEventRepository _eventRepository = _eventRepository;

    private readonly IMapper _mapper = _mapper;

    public async Task<int> Handle(AddEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = _mapper.Map<EventEntity>(request.NewEvent);
        
        var result = await _eventRepository.AddAsync(eventEntity, cancellationToken);

        return result;
    }
}
