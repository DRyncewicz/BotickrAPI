namespace BotickrAPI.Domain.SettingsOptions.Authentication;

public class AuthenticationJwtOptions
{
    public string Audience { get; set; } = string.Empty;

    public bool IncludeErrorDetails { get; set; } = true;
}