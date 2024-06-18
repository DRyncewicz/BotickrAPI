using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BotickrAPI.Persistence.Repositories
{
    public class LocationRepository(IBaseRepository _baseRepository) : ILocationRepository
    {
        private readonly IBaseRepository _baseRepository = _baseRepository;

        public async Task<IEnumerable<LocationEntity>> GetAllAsync()
        {
            return await _baseRepository.GetAll<LocationEntity>().ToListAsync();
        }
    }
}
