using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BotickrAPI.Persistence.Repositories;

public class BookingRepository(IBaseRepository _baseRepository) : IBookingRepository
{
    private readonly IBaseRepository _baseRepository = _baseRepository;

    public async Task<IEnumerable<BookingEntity>> GetByEventId(int eventId, CancellationToken ct)
    {
        return await _baseRepository.GetAll<BookingEntity>()
            .Include(x => x.BookingDetail)
            .Where(x => x.EventId == eventId)
            .ToListAsync(ct);
    }
}
