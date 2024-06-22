using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BotickrAPI.Persistence.Repositories;

internal class TicketRepository(IBaseRepository _baseRepository) : ITicketRepository
{
    private readonly IBaseRepository _baseRepository = _baseRepository;

    public async Task<int> AddAsync(TicketEntity ticketEntity, CancellationToken ct)
    {
        await _baseRepository.AddAsync(ticketEntity, ct);
        await _baseRepository.SaveAsync(ct);

        return ticketEntity.Id;
    }
    public async Task<int> AddRangeAsync(IEnumerable<TicketEntity> ticketEntity, CancellationToken ct)
    {
        await _baseRepository.AddRangeAsync(ticketEntity, ct);
        var id = await _baseRepository.SaveAsync(ct);

        return id;
    }

    public async Task<IEnumerable<TicketEntity>> GetTicketsByEventIdAsync(int eventId, CancellationToken ct)
    {
        return await _baseRepository.GetAll<TicketEntity>().Include(x => x.BookingDetails).Where(x => x.EventId == eventId).ToListAsync(ct);
    }
}