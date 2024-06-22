using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Domain.Repositories;

public interface IBookingRepository
{
    Task<IEnumerable<BookingEntity>> GetByEventId(int eventId, CancellationToken ct);
}
