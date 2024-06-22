using AutoMapper;
using BotickrAPI.Application.Features.Events.Queries.GetEventDetailsById;
using BotickrAPI.Application.UnitTests.MapperProfiles;
using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Enums;
using BotickrAPI.Domain.Exceptions;
using BotickrAPI.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BotickrAPI.Application.UnitTests.Features.Event.Queries.GetEventDetailsById;

public class GetEventDetailsByIdQueryHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;

    private readonly Mock<ITicketRepository> _ticketRepositoryMock;

    private readonly IMapper _mapper;

    private  GetEventDetailsByIdQuery _query;

    private readonly GetEventDetailsByIdQueryHandler _handler;

    private readonly EventEntity _event;

    private readonly List<TicketEntity> _tickets;
    public GetEventDetailsByIdQueryHandlerTests(MappingTestFixture fixture)
    {
        _mapper = fixture.Mapper;
        _eventRepositoryMock = new Mock<IEventRepository>();
        _ticketRepositoryMock = new Mock<ITicketRepository>();
        _handler = new(_mapper, _eventRepositoryMock.Object, _ticketRepositoryMock.Object);

        _event = new()
        {
            Description = "description",
            Duration = TimeSpan.FromSeconds(10),
            Id = 1,
            LocationId = 1,
            Name = "name",
            ImagePath = "test",
            Status = EventStatus.Approved.ToString(),
            OrganizerId = "testid",
            EventType = "TestType"
        };

        _tickets = new List<TicketEntity>()
        {
        new()
        {

            BookingDetails = new List<BookingDetailEntity>()
            {
                new()
                {
                    Id = 1,
                    Quantity = 1,
                    TicketId = 1,
                    BookingId = 1
                },
                new()
                {
                    Id = 2,
                    Quantity = 9,
                    TicketId = 1,
                    BookingId = 2
                }
                },
            Id =1,
            EventId = 1,
            Price = 50,
            Quantity = 110,
            TicketType = "NORMAL"
            }
        };
    }


    [Fact]
    public async Task Handle_ShouldReturnMappedModel_WhenQueryProvided()
    {
        //Arrange
        _eventRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(_event);
        _ticketRepositoryMock.Setup(x => x.GetTicketsByEventIdAsync(It.IsAny<int>(), default)).ReturnsAsync(_tickets);
        _query = new()
        {
            Id = 1
        };

        //Act
        var result = await _handler.Handle(_query, default);

        //Assert
        result.Should().NotBeNull();
        result.Tickets.First().AvailableQuantity.Should().Be(100);
        result.Tickets.First().TotalQuantity.Should().Be(110);
        result.Tickets.First().IsSoldOut.Should().Be(false);
        result.ImagePath.Should().Be(_event.ImagePath);
        result.EventType.Should().Be(_event.EventType); 
        result.Description.Should().Be(_event.Description);
        result.Duration.Should().Be(_event.Duration);
        result.Id.Should().Be(_event.Id);
        result.StartTime.Should().Be(_event.StartTime);
        result.LocationId.Should().Be(_event.LocationId);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenTicketDoesntExist()
    {
        //Arrange
        _eventRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(_event);
        _ticketRepositoryMock.Setup(x => x.GetTicketsByEventIdAsync(It.IsAny<int>(), default)).ReturnsAsync(new List<TicketEntity>()); 
        _query = new()
        {
            Id = 1
        };

        //Act & assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEventtDoesntExist()
    {
        //Arrange
        _eventRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync(() => (EventEntity)null);
        _ticketRepositoryMock.Setup(x => x.GetTicketsByEventIdAsync(It.IsAny<int>(), default)).ReturnsAsync(_tickets);
        _query = new()
        {
            Id = 1
        };

        //Act & assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_query, CancellationToken.None));
    }
}
