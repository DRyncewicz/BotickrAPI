using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Domain.Repositories;

public interface ILocationRepository
{
    Task<IEnumerable<LocationEntity>> GetAllAsync();

    Task<LocationEntity> GetByIdAsync(int locationId, CancellationToken ct);
}
