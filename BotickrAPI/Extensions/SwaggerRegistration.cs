using BotickrAPI.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using SwaggerAppsettings = BotickrAPI.Domain.SettingsOptions.Swagger.SwaggerOptions;

namespace BotickrAPI.Extensions;

public static class SwaggerRegistration
{
    /// <summary>.
    /// Configures swagger and registers it with services.
    /// </summary>
    /// <param name="services">Reference to an instance of <see cref="IServiceCollection"/>.</param>.
    /// <param name="configuration">Configuration for the application.</param>.
    /// <returns><paramref name="services"/> with swagger added.</returns>.
    public static IServiceCollection AddSwagger(this IServiceCollection services,
                                                IConfiguration configuration)
    {
        SwaggerAppsettings swaggerOptions = configuration.GetSection(SwaggerAppsettings.AppsettingsKey).Get<SwaggerAppsettings>()!;

        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<AuthorizeCheckOperationFilter>();

            c.IncludeApiXmlComments();

            c.EnableAnnotations();
            c.UseAllOfToExtendReferenceSchemas();

            c.CustomOperationIds(x =>
            {
                x.ActionDescriptor
                 .RouteValues
                 .TryGetValue("controller", out var controllerName);

                x.ActionDescriptor
                 .RouteValues
                 .TryGetValue("action", out var actionName);

                return $"{controllerName}{actionName}";
            });

            c.AddSecurity(swaggerOptions);
        });
        return services;

    }

    private static void IncludeApiXmlComments(this SwaggerGenOptions c)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        c.IncludeXmlComments(xmlPath);
    }


    private static void AddSecurity(this SwaggerGenOptions c, SwaggerAppsettings swaggerOptions)
    {
        c.OperationFilter<AuthorizeCheckOperationFilter>();
        c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme.ToLower(), new OpenApiSecurityScheme()
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows()
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(swaggerOptions.AuthorizationUrl),
                    TokenUrl = new Uri(swaggerOptions.TokenUrl),
                    Scopes = swaggerOptions.Scopes
                }
            }
        });
    }

    public static WebApplication UseSwaggerConfig(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Botick v1");
            c.OAuthClientId("swagger");
            c.OAuthClientSecret("secret");
            c.OAuthUsePkce();
            c.OAuth2RedirectUrl("https://localhost:7008/swagger/oauth2-redirect.html");

        });

        return app;
    }
}