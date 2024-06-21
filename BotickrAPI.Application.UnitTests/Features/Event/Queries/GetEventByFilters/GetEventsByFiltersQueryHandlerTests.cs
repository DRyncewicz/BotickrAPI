using AutoMapper;
using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Application.Features.Events.Queries.GetEventsByFilters;
using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace BotickrAPI.Application.UnitTests.Features.Event.Queries.GetEventByFilters;

public class GetEventsByFiltersQueryHandlerTests
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetEventsByFiltersQueryHandler _handler;

    public GetEventsByFiltersQueryHandlerTests()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetEventsByFiltersQueryHandler(_mapperMock.Object, _eventRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedEvents_WhenFiltersAreApplied()
    {
        // Arrange
        var query = new GetEventsByFiltersQuery
        {
            SearchString = "test",
            EventStart = DateTime.Now,
            LocationId = 1,
            PageSize = 10,
            PageIndex = 1
        };

        var events = new List<EventEntity>
    {
        new EventEntity { Id = 1, Name = "Test Event", StartTime = DateTime.Now, Duration = TimeSpan.FromHours(2) }
    };
        var eventDtos = new List<EventDto>
    {
        new EventDto { Name = "Test Event", StartTime = DateTime.Now, Duration = TimeSpan.FromHours(2) }
    };

        _eventRepositoryMock.Setup(repo => repo.GetByFiltersAsync(query.SearchString, query.EventStart, query.LocationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(events);

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<EventDto>>(events))
            .Returns(eventDtos);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result.Should().BeEquivalentTo(eventDtos);
    }

    [Fact]
    public async Task Handle_ShouldReturnPagedEvents_WhenPageSizeAndPageIndexAreProvided()
    {
        // Arrange
        var query = new GetEventsByFiltersQuery
        {
            SearchString = "test",
            EventStart = DateTime.Now,
            LocationId = 1,
            PageSize = 1,
            PageIndex = 1
        };

        var events = new List<EventEntity>
    {
        new EventEntity { Id = 1, Name = "Test Event 1", StartTime = DateTime.Now, Duration = TimeSpan.FromHours(2) },
        new EventEntity { Id = 2, Name = "Test Event 2", StartTime = DateTime.Now.AddDays(1), Duration = TimeSpan.FromHours(3) }
    };

        var pagedEvents = events.Take(query.PageSize);
        var eventDtos = new List<EventDto>
    {
        new EventDto { Name = "Test Event 1", StartTime = DateTime.Now, Duration = TimeSpan.FromHours(2) }
    };

        _eventRepositoryMock.Setup(repo => repo.GetByFiltersAsync(query.SearchString, query.EventStart, query.LocationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(events);

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<EventDto>>(pagedEvents))
            .Returns(eventDtos);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result.Should().BeEquivalentTo(eventDtos);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoEventsMatchFilters()
    {
        // Arrange
        var query = new GetEventsByFiltersQuery
        {
            SearchString = "nonexistent",
            EventStart = DateTime.Now,
            LocationId = 1,
            PageSize = 10,
            PageIndex = 1
        };

        var events = new List<EventEntity>();
        var eventDtos = new List<EventDto>();

        _eventRepositoryMock.Setup(repo => repo.GetByFiltersAsync(query.SearchString, query.EventStart, query.LocationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(events);

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<EventDto>>(events))
            .Returns(eventDtos);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}