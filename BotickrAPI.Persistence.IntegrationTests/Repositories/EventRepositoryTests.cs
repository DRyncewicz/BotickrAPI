using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Enums;
using BotickrAPI.Persistence.DbContext;
using BotickrAPI.Persistence.IntegrationTests.TestConfiguration;
using BotickrAPI.Persistence.Repositories;
using FluentAssertions;
using Xunit;

namespace BotickrAPI.Persistence.IntegrationTests.Repositories;

public class EventRepositoryTests : IDisposable
{
    private DatabaseContext _dbContext;

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    private List<ArtistEntity> _artists = new()
    {
        new()
        {
            Description = "TestDescription1",
            ArtName = "TestArtName1",
            Name = "TestName1",
            Surname = "TestSurname1",
            Likes = 50,
            Discipline = "TestDiscipline1",
            ImagePath = "TestImagePath1"
        },
        new()
        {
            Description = "TestDescription2",
            Name = "TestName2",
            Surname = "TestSurname2",
            Likes = 100,
            Discipline = "TestDiscipline2",
            ImagePath = "TestImagePath2"
        }
    };
    private LocationEntity location = new()
    {
        City = "TestCity",
        Capacity = 50,
        Venue = "TestVenue"
    };

    private List<EventEntity> _events = new()
    {
        new()
        {
            Name = "TestNameEvent1",
            Description = "TestDescription1",
            Duration = TimeSpan.FromMinutes(30),
            LocationId = 1,
            EventType = "Concert",
            StartTime = new DateTime(2024, 1,1),
            Status = EventStatus.Waiting.ToString(),
            ImagePath = "TestImagePath",
            OrganizerId = "TESTORGANIZERID"
        },
        new()
        {
            Name = "TestNameEvent2",
            Description = "TestDescription2",
            Duration = TimeSpan.FromMinutes(60),
            LocationId = 1,
            EventType = "Concert",
            StartTime = new DateTime(2025, 1,1),
            Status = EventStatus.Waiting.ToString(),
            ImagePath = "TestImagePath",
            OrganizerId = "TESTORGANIZERID"
        }
    };

    private List<EventArtistsEntity> _eventArtists = new()
    {
        new()
        {
            ArtistId = 1,
            EventId = 1,
        },
        new()
        {
            EventId = 1,
            ArtistId = 2
        },
        new()
        {
            EventId = 2,
            ArtistId = 1
        }
    };

    private async Task SeedData(DatabaseContext dbContext)
    {
        await dbContext.Artists.AddRangeAsync(_artists, default);
        await dbContext.SaveChangesAsync(default);
        await dbContext.Locations.AddAsync(location, default);
        await dbContext.SaveChangesAsync(default);
        await dbContext.Events.AddRangeAsync(_events, default);
        await dbContext.SaveChangesAsync(default);
        await dbContext.EventArtists.AddRangeAsync(_eventArtists, default);
        await dbContext.SaveChangesAsync(default);
    }

    [Theory]
    [InlineData(null, null, null, 2)]
    [InlineData("TestNameEvent1", null, null, 1)]
    [InlineData("TestNameEvent2", null, 1, 1)]
    [InlineData(null, null, 2, 0)]
    [InlineData(null, null, 1, 2)]

    public async Task GetByFiltersAsync_ShouldReturnRecordsBaseOnFilters(string? searchString, DateTime? EventStartDate, int? locationId, int AssertCount)
    {
        //Arrange
        _dbContext = DatabaseTestFixture.CreateLocalSqlServerDatabaseContext("GetByFiltersAsync_ShouldReturnRecordsBaseOnFilters");
        await SeedData(_dbContext);

        var repository = new EventRepository(new BaseRepository(_dbContext));

        //Act
        var result = await repository.GetByFiltersAsync(searchString, EventStartDate, locationId, default);

        //Assert
        result.Should().HaveCount(AssertCount);
    }

    [Theory]
    [InlineData(2024, 1, 1, 1)]
    [InlineData(2025, 1, 1, 1)]
    [InlineData(2020, 1, 1, 0)]
    public async Task GetByFiltersAsync_ShouldReturnRecordsBaseOnDateFilter(int year, int month, int day, int AssertCount)
    {
        //Arrange
        _dbContext = DatabaseTestFixture.CreateLocalSqlServerDatabaseContext("GetByFiltersAsync_ShouldReturnRecordsBaseOnDateFilter");
        await SeedData(_dbContext);
        var date = new DateTime(year, month, day);

        var repository = new EventRepository(new BaseRepository(_dbContext));

        //Act
        var result = await repository.GetByFiltersAsync(null, date, null, default);

        //Assert
        result.Should().HaveCount(AssertCount);
    }
}