using BotickrAPI.Domain.Transactions;
using BotickrAPI.Persistence.DbContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace BotickrAPI.Persistence.Transactions;

public class DatabaseTransaction(DatabaseContext _dbContext) : IDatabaseTransaction
{
    private readonly DatabaseContext _dbContext = _dbContext;

    public async Task<IDbContextTransaction> BeginAsync(CancellationToken ct = default)
    {
        return await _dbContext.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct = default)
    {
        await _dbContext.Database.CommitTransactionAsync(ct);
    }

    public async Task RollbackAsync(CancellationToken ct = default)
    {
        await _dbContext.Database.RollbackTransactionAsync(ct);
    }
}
