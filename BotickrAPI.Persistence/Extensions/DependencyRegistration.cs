using BotickrAPI.Domain.Repositories;
using BotickrAPI.Domain.Transactions;
using BotickrAPI.Persistence.DbContext;
using BotickrAPI.Persistence.Repositories;
using BotickrAPI.Persistence.Transactions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotickrAPI.Application.Extensions;

public static class DependencyRegistration
{
    public static IServiceCollection AddPersistenceDI(this IServiceCollection services,
                                              IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
        services.AddScoped<IBaseRepository, BaseRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IEventArtistsRepository, EventArtistsRepository>();
        services.AddScoped<IDatabaseTransaction, DatabaseTransaction>();

        return services;
    }
    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();
            dbContext?.Migrate();
        }

        return app;
    }
}
