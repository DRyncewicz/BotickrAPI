namespace BotickrAPI.Domain.SettingsOptions.Swagger;

public class SwaggerOptions
{
    public const string AppsettingsKey = "Swagger";

    public string AuthorizationUrl { get; set; } = string.Empty;

    public string TokenUrl { get; set; } = string.Empty;

    public Dictionary<string, string> Scopes { get; set; } = [];
}