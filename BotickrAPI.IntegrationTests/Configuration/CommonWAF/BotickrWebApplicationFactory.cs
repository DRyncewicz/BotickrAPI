using BotickrAPI.Persistence.DbContext;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BotickrAPI.IntegrationTests.Configuration.CommonWAF
{
    public class BotickrWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IDisposable where TProgram : class
    {
        public const string dbName = "BotickrAPIIntegrationTestsDb";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DatabaseContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                }
            });
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                using (var scope = Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var applicationDbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    applicationDbContext.Database.EnsureDeleted();
                    GC.SuppressFinalize(this);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong on disposing test database unit: " + ex.Message);
            }

            base.Dispose(disposing);
        }
    }
}