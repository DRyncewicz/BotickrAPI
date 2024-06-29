namespace BotickrAPI.Domain.SettingsOptions.Authentication;

public class AuthenticationOptions
{
    public const string AppsettingsKey = "Authentication";

    public AuthenticationDuendeOptions Duende { get; set; } = new();

    public AuthenticationJwtOptions Jwt { get; set; } = new();
}