using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Domain.Repositories;

public interface ILocationRepository
{
    Task<IEnumerable<LocationEntity>> GetAllAsync();
}
