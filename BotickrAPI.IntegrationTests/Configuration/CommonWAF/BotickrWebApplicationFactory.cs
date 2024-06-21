using BotickrAPI.Persistence.DbContext;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BotickrAPI.IntegrationTests.Configuration.CommonWAF
{
    public class BotickrWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private string _dbName;

        public BotickrWebApplicationFactory(string dbName)
        {
            _dbName = dbName;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"ConnectionStrings:DefaultConnectionString", $"Server=(localdb)\\mssqllocaldb;Database={_dbName};Trusted_Connection=True;MultipleActiveResultSets=true"}
                });
            });
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

    }
}