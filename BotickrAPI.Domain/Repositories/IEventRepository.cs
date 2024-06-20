using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Domain.Repositories;

public interface IEventRepository
{
    Task<int> AddAsync(EventEntity eventEntity, CancellationToken ct);
}
