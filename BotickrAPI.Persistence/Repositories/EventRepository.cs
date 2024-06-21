using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BotickrAPI.Persistence.Repositories;

public class EventRepository(IBaseRepository _baseRepository) : IEventRepository
{
    private readonly IBaseRepository _baseRepository = _baseRepository;

    public async Task<int> AddAsync(EventEntity eventEntity, CancellationToken ct)
    {
        await _baseRepository.AddAsync(eventEntity, ct);
        await _baseRepository.SaveAsync(ct);

        return eventEntity.Id;
    }

    public async Task<IEnumerable<EventEntity>> GetByFiltersAsync(string? searchString, DateTime? eventDate, int? locationId, CancellationToken ct)
    {
        var query = _baseRepository.GetAll<EventEntity>()
            .AsNoTracking()
            .Include(p => p.EventArtists)
            .ThenInclude(p => p.Artist);

        if (searchString != null)
        {
            query = query.Where(p => p.Name.ToLower().Contains(searchString.ToLower())
                           || p.EventArtists.Any(ea => ea.Artist.Name.ToLower().Contains(searchString.ToLower()))
                           || p.EventArtists.Any(ea => ea.Artist.Surname.ToLower().Contains(searchString.ToLower()))
                           || p.EventArtists.Any(ea => ea.Artist.ArtName.ToLower().Contains(searchString.ToLower())))
                            .Include(p => p.EventArtists)
                            .ThenInclude(p => p.Artist); 
        }

        if (eventDate.HasValue)
        {
            query = query.Where(e => e.StartTime.Date == eventDate.Value.Date)
                         .Include(p => p.EventArtists)
                         .ThenInclude(p => p.Artist);
        }

        if (locationId.HasValue)
        {
            query = query.Where(e => e.LocationId == locationId.Value)
                        .Include(p => p.EventArtists)
                        .ThenInclude(p => p.Artist);
        }

        return await query.OrderBy(p => p.StartTime).ToListAsync(ct);
    }
}
