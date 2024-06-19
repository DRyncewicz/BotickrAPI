using BotickrAPI.Domain.Repositories;
using BotickrAPI.Persistence.DbContext;

namespace BotickrAPI.Persistence.Repositories;

public class BaseRepository(DatabaseContext _dbContext) : IBaseRepository
{
    private readonly DatabaseContext _dbContext = _dbContext;

    public async Task AddAsync<TEntity>(TEntity entity, CancellationToken ct = default) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        await _dbContext.AddAsync(entity, ct);
    }

    public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken ct) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));

        await _dbContext.AddRangeAsync(entities);
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        _dbContext.Remove(entity);
    }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => _dbContext.Set<TEntity>();

    public async Task<int> SaveAsync(CancellationToken ct = default)
        => await _dbContext.SaveChangesAsync(ct);

    public void Update<TEntity>(TEntity entity) where TEntity : class
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        _dbContext.Update(entity);
    }
}