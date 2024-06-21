

using Microsoft.Extensions.Configuration;

namespace BotickrAPI.Persistence.IntegrationTests.TestConfiguration;

public class ConfigurationProviderHelper
{
    /// <summary>
    /// Inicjalizuje konfigurację z pliku appsettings.tests.json.
    /// </summary>
    /// <returns>Instację <see cref="IConfiguration"/></returns>
    public static IConfiguration InitializeConfiguration()
        => new ConfigurationBuilder().AddJsonFile(Path.Combine("TestConfiguration", "appsettings.tests.json"))
                                     .AddEnvironmentVariables()
                                     .Build();
}