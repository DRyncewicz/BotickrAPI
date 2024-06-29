using BotickrAPI.Domain.SettingsOptions.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace BotickrAPI.Application.Extensions;

internal static class AuthorizationRegistration
{
    internal static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
                                                            AuthenticationOptions authenticationOptions)
    {
        AddJWTAuthentication(services, authenticationOptions);
        services.AddAuthorizationBuilder()
                .AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });

        return services;
    }

    private static void AddJWTAuthentication(this IServiceCollection services,
                                                   AuthenticationOptions authenticationOptions)
    {
        services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = authenticationOptions.Duende.AuthorityServerAddress;
                    options.Audience = authenticationOptions.Jwt.Audience;
                    options.IncludeErrorDetails = authenticationOptions.Jwt.IncludeErrorDetails;
                });
    }
}

