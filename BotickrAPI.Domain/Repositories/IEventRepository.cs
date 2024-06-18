using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Domain.Repositories;

public interface IEventRepository
{
    Task<int> AddAsnyc(EventEntity eventEntity, CancellationToken ct);
}
