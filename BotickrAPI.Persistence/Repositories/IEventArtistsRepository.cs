using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;

namespace BotickrAPI.Persistence.Repositories;

public class EventArtistsRepository(IBaseRepository _baseRepository) : IEventArtistsRepository
{
    private readonly IBaseRepository _baseRepository = _baseRepository;

    public async Task<int> AddAsync(EventArtistsEntity eventArtistsEntity, CancellationToken ct)
    {
        await _baseRepository.AddAsync(eventArtistsEntity, ct);
        await _baseRepository.SaveAsync(ct);

        return eventArtistsEntity.EventId;
    }

    public async Task<bool> AddRangeAsync(IEnumerable<EventArtistsEntity> eventArtistsEntities, CancellationToken ct)
    {
        await _baseRepository.AddRangeAsync(eventArtistsEntities, ct);
        await _baseRepository.SaveAsync(ct);

        return true;
    }
}
