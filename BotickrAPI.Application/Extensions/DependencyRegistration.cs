using BotickrAPI.Application.Abstractions.Services;
using BotickrAPI.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotickrAPI.Application.Extensions;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services,
                                              IConfiguration configuration)
    {
        services.AddScoped<IDateTimeService, DateTimeService>();

        return services;
    }

}
