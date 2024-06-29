using BotickrAPI.Application.Abstractions.Services;
using BotickrAPI.Application.Services;
using BotickrAPI.Domain.SettingsOptions.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace BotickrAPI.Application.Extensions;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services,
                                              IConfiguration configuration)
    {
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.AppsettingsKey));
        services.AddJwtAuthentication(configuration.GetSection(AuthenticationOptions.AppsettingsKey).Get<AuthenticationOptions>()!);

        return services;
    }
}
