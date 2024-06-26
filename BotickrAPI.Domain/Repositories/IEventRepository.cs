﻿using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Domain.Repositories;

public interface IEventRepository
{
    Task<int> AddAsync(EventEntity eventEntity, CancellationToken ct);

    Task<IEnumerable<EventEntity>> GetByFiltersAsync(string? searchString, DateTime? eventDate, int? locationId, CancellationToken ct);

    Task<EventEntity> GetByIdAsync(int eventId, CancellationToken ct);
}
