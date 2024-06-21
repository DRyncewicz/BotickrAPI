using BotickrAPI.Application.Services;
using BotickrAPI.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace BotickrAPI.Persistence.IntegrationTests.TestConfiguration;

internal class DatabaseTestFixture
{
    private static string ConnectionString(string databaseName) => $"Server=(localdb)\\mssqllocaldb; Database={databaseName}";

    public static DatabaseContext CreateLocalSqlServerDatabaseContext(string databaseName)
    {
        DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer(ConnectionString(databaseName)).Options;
        DatabaseContext context = new(options, new DateTimeService());
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }
}