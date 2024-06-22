using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Application.Dtos.Tickets;
using BotickrAPI.Application.Features.Events.Commands.AddEvent;
using BotickrAPI.Application.Features.Events.Queries.GetEventDetailsById;
using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Enums;
using BotickrAPI.IntegrationTests.Configuration.CommonWAF;
using BotickrAPI.Persistence.DbContext;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace BotickrAPI.IntegrationTests.Controller;

public class AddEventCommandHandlerTests :  IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly BotickrWebApplicationFactory<Program> _factory;

    public AddEventCommandHandlerTests()
    {
        _factory = new BotickrWebApplicationFactory<Program>(nameof(AddEventCommandHandlerTests));
        _httpClient = _factory.CreateClient();
        SeedDatabase();
    }

    #region GetEventsByFilters
    [Fact]
    public async Task GetEventsByFilters_ShouldReturnFilteredEvents()
    {
        // Arrange
        var query = new
        {
            SearchString = "Test",
            LocationId = 1,
            PageSize = 10,
            PageIndex = 1
        };

        var queryString = $"?LocationId={query.LocationId}&SearchString={query.SearchString}&PageIndex={query.PageIndex}&PageSize={query.PageSize}";

        // Act
        var response = await _httpClient.GetAsync($"/api/Event/GetByFilters{queryString}");
        var json = await response.Content.ReadAsStringAsync();
        var result = string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<IEnumerable<EventDto>>(json);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(e => e.Name.Contains("Test Event"));
    }

    [Fact]
    public async Task GetEventsByFilters_ShouldReturnPagedEvents()
    {
        // Arrange
        var query = new
        {
            SearchString = "Test",
            EventStart = DateTime.Now.AddDays(1).ToString("MM-dd-yyyy"),
            LocationId = 1,
            PageSize = 1,
            PageIndex = 1
        };

        var queryString = $"?LocationId={query.LocationId}&SearchString={query.SearchString}&EventStart={query.EventStart}&PageIndex={query.PageIndex}&PageSize={query.PageSize}";

        // Act
        var response = await _httpClient.GetAsync($"/api/Event/GetByFilters{queryString}");
        var json = await response.Content.ReadAsStringAsync();
        var result = string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<IEnumerable<EventDto>>(json);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result.Should().Contain(e => e.Name == "Test Event 1");
    }

    [Fact]
    public async Task GetEventsByFilters_ShouldReturnEmptyList_WhenNoEventsMatchFilters()
    {
        // Arrange
        var query = new
        {
            SearchString = "Nonexistent",
            EventStart = DateTime.Now.AddDays(1).ToString("MM-dd-yyyy"),
            LocationId = 1,
            PageSize = 10,
            PageIndex = 1
        };

        var queryString = $"?LocationId={query.LocationId}&SearchString={query.SearchString}&EventStart={query.EventStart}&PageIndex={query.PageIndex}&PageSize={query.PageSize}";

        // Act
        var response = await _httpClient.GetAsync($"/api/Event/GetByFilters{queryString}");
        var json = await response.Content.ReadAsStringAsync();
        var result = string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<IEnumerable<EventDto>>(json);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
    #endregion

    #region AddEvent
    [Fact]
    public async Task AddEvent_ShouldReturnInternalServerError_WhenExceptionOccurs()
    {
        // Arrange
        var command = new AddEventCommand
        {
            NewEvent = new NewEventDto
            {
                Name = "New Test Event",
                EventType = "Concert",
                StartTime = DateTime.Now.AddDays(30),
                Duration = TimeSpan.FromHours(2),
                LocationId = 99,
                ArtistIds = new List<int> { 1, 2 },
                TicketDtos = new List<NewTicketDto>
                {
                    new NewTicketDto { Price = 50, Quantity = 200, TicketType = "Standard" }
                }
            }
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Event", command);
        var json = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddEvent_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var invalidCommand = new AddEventCommand
        {
            NewEvent = new NewEventDto
            {
            }
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Event", invalidCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task AddEvent_ShouldCreateNewEventAndReturnId()
    {
        // Arrange
        var command = new AddEventCommand
        {
            NewEvent = new NewEventDto
            {
                Name = "New Test Event",
                EventType = "Concert",
                StartTime = DateTime.Now.AddDays(30),
                Duration = TimeSpan.FromHours(2),
                LocationId = 1,
                ArtistIds = new List<int> { 1, 2 },
                TicketDtos = new List<NewTicketDto>
                {
                    new NewTicketDto { Price = 50, Quantity = 200, TicketType = "Standard" }
                }
            }
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Event", command);
        var json = await response.Content.ReadAsStringAsync();
        var eventId = string.IsNullOrEmpty(json) ? 0 : JsonConvert.DeserializeObject<int>(json);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        eventId.Should().BeGreaterThan(0);
    }

    #endregion

    #region GetDetailsById

    [Fact]
    public async Task GetDetailsById_ShouldReturnsOk()
    {
        // Arrange
        var query = new GetEventDetailsByIdQuery
        {
            Id = 1
        };

        // Act
        var response = await _httpClient.GetAsync($"/api/Event/{query.Id}/GetDetails");
        var json = await response.Content.ReadAsStringAsync();
        var result = string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<DetailEventInfoDto>(json);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Description.Should().Be("Description1");
        result.Duration.Should().Be(TimeSpan.FromHours(2));
        result.Tickets.First().IsSoldOut.Should().Be(false);
        result.Tickets.First().TotalQuantity.Should().Be(110);
        result.Tickets.First().AvailableQuantity.Should().Be(10);
        result.Tickets.First().Price.Should().Be(25);

    }

    [Fact]
    public async Task GetDetailsById_ShouldReturnNotFound_OnNotExistingEventId()
    {
        // Arrange
        var query = new GetEventDetailsByIdQuery
        {
            Id = 1125
        };

        // Act
        var response = await _httpClient.GetAsync($"/api/Event/{query.Id}/GetDetails");
        var json = await response.Content.ReadAsStringAsync();
        var result = string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<DetailEventInfoDto>(json);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetDetailsById_ShouldReturnOk_OnExistingEventWithoutBookedTickets()
    {
        // Arrange
        var query = new GetEventDetailsByIdQuery
        {
            Id = 2
        };

        // Act
        var response = await _httpClient.GetAsync($"/api/Event/{query.Id}/GetDetails");
        var json = await response.Content.ReadAsStringAsync();
        var result = string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<DetailEventInfoDto>(json);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Tickets.First().IsSoldOut.Should().Be(false);
        result.Tickets.First().TotalQuantity.Should().Be(1500);
        result.Tickets.First().AvailableQuantity.Should().Be(1500);
        result.Tickets.First().Price.Should().Be(10);
    }
    #endregion

    private void SeedDatabase()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            var location = new LocationEntity { City = "Test City", Capacity = 1000, Venue = "Test Venue" };
            dbContext.Locations.Add(location);
            dbContext.SaveChanges();

            var event1 = new EventEntity
            {
                Name = "Test Event 1",
                EventType = "Concert",
                StartTime = DateTime.Now.AddDays(1),
                Duration = TimeSpan.FromHours(2),
                LocationId = 1,
                OrganizerId = "Test",
                Status = EventStatus.Waiting.ToString(),
                Description = "Description1",
                ImagePath = "ImagePath1"
            };

            var event2 = new EventEntity
            {
                Name = "Test Event 2",
                EventType = "Concert",
                StartTime = DateTime.Now.AddDays(2),
                Duration = TimeSpan.FromHours(3),
                LocationId = 1,
                OrganizerId = "Test",
                Status = EventStatus.Waiting.ToString(),
                Description = "Description2",
                ImagePath = "ImagePath1"
            };

            dbContext.Events.AddRange(event1, event2);
            dbContext.SaveChanges();

            var artist1 = new ArtistEntity
            {
                Description = "description1",
                ArtName = "Art1",
                Name = "Name1",
                Surname = "Surname1",
                Likes = 50,
                Discipline = "Testd1",
                ImagePath = "TestImagePath"
            };
            var artist2 = new ArtistEntity
            {
                Description = "description2",
                ArtName = "Art2",
                Name = "Name2",
                Surname = "Surname2",
                Likes = 505,
                Discipline = "Testd2",
                ImagePath = "TestImagePath"

            };
            dbContext.Artists.AddRange(artist1, artist2);
            dbContext.SaveChanges();

            var ticket1 = new TicketEntity
            {
                EventId = 1,
                Quantity = 110,
                TicketType = "VIP",
                Price = 25
            };
            var ticket2 = new TicketEntity
            {
                EventId = 2,
                Quantity = 1500,
                TicketType = "SINGLE",
                Price = 10
            };
            dbContext.Tickets.AddRange(ticket1, ticket2);
            dbContext.SaveChanges();

            var booking1 = new BookingEntity
            {
                BookingTime = new DateTime(2024, 1, 1),
                EventId = 1,
                TotalPrice = 2500,
                Status = "Confirmed",
                UserId = "UserId"
            };
            dbContext.Bookings.Add(booking1);
            dbContext.SaveChanges();

            var bookingDetails1 = new BookingDetailEntity
            {
                BookingId = 1,
                TicketId = 1,
                Quantity = 100
            };

            dbContext.BookingDetails.Add(bookingDetails1);
            dbContext.SaveChanges();
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