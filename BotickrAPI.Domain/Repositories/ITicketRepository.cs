using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Domain.Repositories;

public interface ITicketRepository
{
    Task<int> AddAsync(TicketEntity ticketEntity, CancellationToken ct);

    Task<int> AddRangeAsync(IEnumerable<TicketEntity> ticketEntity, CancellationToken ct);

    Task<IEnumerable<TicketEntity>> GetTicketsByEventIdAsync(int eventId, CancellationToken ct);
}
