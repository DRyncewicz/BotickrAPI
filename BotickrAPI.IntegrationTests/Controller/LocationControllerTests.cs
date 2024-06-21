using BotickrAPI.Application.Dtos.Locations;
using BotickrAPI.Domain.Entities;
using BotickrAPI.IntegrationTests.Configuration.CommonWAF;
using BotickrAPI.Persistence.DbContext;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace BotickrAPI.IntegrationTests.Controller;


public class LocationControllerTests : IDisposable
{
    private readonly HttpClient _unauthorizedHttpClient;

    private BotickrWebApplicationFactory<Program> _factory;

    public LocationControllerTests()
    {
        _factory = new BotickrWebApplicationFactory<Program>(nameof(LocationControllerTests));
        _unauthorizedHttpClient = _factory.CreateClient();
        Seed();
    }

    [Fact]
    public async Task GetAll_shouldReturnAllRecords()
    {
        //Act
        var response = await _unauthorizedHttpClient.GetAsync($"/api/Location");
        var json = await response.Content.ReadAsStringAsync();
        var result = string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<IEnumerable<LocationDto>>(json);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        result.Should().HaveCount(2);
    }
    private void Seed()
    {
        using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            LocationEntity location = new()
            {
                City = "TestCity",
                Capacity = 50,
                Venue = "TestVenue"
            };
            LocationEntity location2 = new()
            {
                City = "TestCity2",
                Capacity = 550,
                Venue = "TestVenue2"
            };

            applicationDbContext.Locations.AddRange(location, location2);
            applicationDbContext.SaveChanges();
        }
    }
    public void Dispose()
    {
        using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            applicationDbContext.Database.EnsureDeleted();
            GC.SuppressFinalize(this);
        }
    }
}
