namespace BotickrAPI.Domain.Repositories;

public interface IBaseRepository
{

    IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

    Task AddAsync<TEntity>(TEntity entity, CancellationToken ct) where TEntity : class;

    void Update<TEntity>(TEntity entity) where TEntity : class;

    void Delete<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveAsync(CancellationToken ct);
}