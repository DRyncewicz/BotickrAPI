using Microsoft.EntityFrameworkCore.Storage;

namespace BotickrAPI.Domain.Transactions;

public interface IDatabaseTransaction
{
    /// <summary>
    /// Rozpoczęcie transakcji
    /// </summary>
    /// <returns>IDbContextTransaction</returns>
    Task<IDbContextTransaction> BeginAsync(CancellationToken ct = default);

    /// <summary>
    /// Zatwierdzenie transakcji
    /// </summary>
    /// <returns></returns>
    Task CommitAsync(CancellationToken ct = default);

    /// <summary>
    /// Wycofanie transakcji
    /// </summary>
    /// <returns></returns>
    Task RollbackAsync(CancellationToken ct = default);
}
