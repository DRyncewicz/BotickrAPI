using BotickrAPI.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotickrAPI.Application.Extensions;

public static class DependencyRegistration
{
    public static IServiceCollection AddPersistenceDI(this IServiceCollection services,
                                              IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

}
