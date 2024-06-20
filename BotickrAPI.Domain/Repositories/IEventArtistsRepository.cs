using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Domain.Repositories;

public interface IEventArtistsRepository
{
    Task<int> AddAsync(EventArtistsEntity eventArtistsEntity, CancellationToken ct);

    Task<bool> AddRangeAsync(IEnumerable<EventArtistsEntity> eventArtistsEntities, CancellationToken ct);
}
