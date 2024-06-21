using AutoMapper;
using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Application.Dtos.Tickets;
using BotickrAPI.Application.Features.Events.Commands.AddEvent;
using BotickrAPI.Application.UnitTests.MapperProfiles;
using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;
using BotickrAPI.Domain.Transactions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Xunit;

namespace BotickrAPI.Application.UnitTests.Features.Event.Commands.AddEvent;

public class AddEventCommandHandlerTests : IClassFixture<MappingTestFixture>
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly Mock<IEventArtistsRepository> _eventArtistsRepositoryMock;
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;
    private readonly IMapper _mapper;
    private readonly Mock<IDatabaseTransaction> _databaseTransactionMock;
    private readonly AddEventCommandHandler _handler;
    private readonly Mock<IDbContextTransaction> _mockTransaction;

    public AddEventCommandHandlerTests(MappingTestFixture fixture)
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _eventArtistsRepositoryMock = new Mock<IEventArtistsRepository>();
        _ticketRepositoryMock = new Mock<ITicketRepository>();
        _mapper = fixture.Mapper;
        _databaseTransactionMock = new Mock<IDatabaseTransaction>();
        _mockTransaction = new Mock<IDbContextTransaction>();
        _handler = new AddEventCommandHandler(
            _eventRepositoryMock.Object,
            _eventArtistsRepositoryMock.Object,
            _ticketRepositoryMock.Object,
            _mapper,
            _databaseTransactionMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldAddEventAndReturnEventId()
    {
        // Arrange
        var newEventDto = new NewEventDto
        {
            Name = "Test Event",
            ArtistIds = new List<int> { 1, 2 },
            TicketDtos = new List<NewTicketDto>
        {
            new NewTicketDto { Price = 100, Quantity = 10, TicketType = "VIP" }
        }
        };
        var command = new AddEventCommand { NewEvent = newEventDto };
        var eventEntity = new EventEntity { Id = 1 };
        var eventArtistsEntities = new List<EventArtistsEntity>
    {
        new EventArtistsEntity { ArtistId = 1, EventId = 1 },
        new EventArtistsEntity { ArtistId = 2, EventId = 1 }
    };
        var ticketEntities = new List<TicketEntity>
    {
        new TicketEntity { Price = 100, Quantity = 10, TicketType = "VIP", EventId = 1 }
    };

        _eventRepositoryMock.Setup(r => r.AddAsync(It.IsAny<EventEntity>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _eventArtistsRepositoryMock.Setup(r => r.AddRangeAsync(It.IsAny<IEnumerable<EventArtistsEntity>>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _ticketRepositoryMock.Setup(r => r.AddRangeAsync(It.IsAny<IEnumerable<TicketEntity>>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _databaseTransactionMock.Setup(x => x.BeginAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockTransaction.Object);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(1);
        _eventRepositoryMock.Verify(r => r.AddAsync(It.IsAny<EventEntity>(), It.IsAny<CancellationToken>()), Times.Once);
        _eventArtistsRepositoryMock.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<EventArtistsEntity>>(), It.IsAny<CancellationToken>()), Times.Once);
        _ticketRepositoryMock.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<TicketEntity>>(), It.IsAny<CancellationToken>()), Times.Once);
        _databaseTransactionMock.Verify(t => t.BeginAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockTransaction.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRollbackTransactionOnException()
    {
        // Arrange
        var newEventDto = new NewEventDto
        {
            Name = "Test Event",
            ArtistIds = new List<int> { 1, 2 },
            TicketDtos = new List<NewTicketDto>
        {
            new NewTicketDto { Price = 100, Quantity = 10, TicketType = "VIP" }
        }
        };
        var command = new AddEventCommand { NewEvent = newEventDto };
        var eventEntity = new EventEntity { Id = 1 };

        _eventRepositoryMock.Setup(r => r.AddAsync(eventEntity, It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());
        _databaseTransactionMock.Setup(t => t.BeginAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_mockTransaction.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        _mockTransaction.Verify(x => x.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
