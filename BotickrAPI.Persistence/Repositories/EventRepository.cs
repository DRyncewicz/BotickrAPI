using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;

namespace BotickrAPI.Persistence.Repositories;

public class EventRepository(IBaseRepository _baseRepository) : IEventRepository
{
    private readonly IBaseRepository _baseRepository = _baseRepository;

    public async Task<int> AddAsnyc(EventEntity eventEntity, CancellationToken ct)
    {
        await _baseRepository.AddAsync(eventEntity, ct);  
        await _baseRepository.SaveAsync(ct);

        return eventEntity.Id;
    }
}
