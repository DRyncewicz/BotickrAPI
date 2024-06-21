using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Application.Dtos.Tickets;
using BotickrAPI.Application.Features.Events.Commands.AddEvent;
using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Repositories;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace BotickrAPI.Application.UnitTests.Features.Event.Commands.AddEvent;

public class AddEventCommandValidatorTests
{
    private readonly Mock<ILocationRepository> _locationRepositoryMock;
    private readonly AddEventCommandValidator _validator;

    public AddEventCommandValidatorTests()
    {
        _locationRepositoryMock = new Mock<ILocationRepository>();
        _validator = new AddEventCommandValidator(_locationRepositoryMock.Object);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Short()
    {
        // Arrange
        var command = new AddEventCommand
        {
            NewEvent = new NewEventDto
            {
                Name = "Short"
            }
        };
        _locationRepositoryMock.Setup(repo => repo.GetByIdAsync(command.NewEvent.LocationId, default))
            .ReturnsAsync(new LocationEntity { Capacity = 50 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(e => e.NewEvent.Name)
            .WithErrorMessage("Length should be greater than 10 chars");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Long()
    {
        // Arrange
        var command = new AddEventCommand
        {
            NewEvent = new NewEventDto
            {
                Name = new string('a', 51)
            }
        };
        _locationRepositoryMock.Setup(repo => repo.GetByIdAsync(command.NewEvent.LocationId, default))
            .ReturnsAsync(new LocationEntity { Capacity = 50 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(e => e.NewEvent.Name)
            .WithErrorMessage("Length should be lower than 50 chars");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Contains_Invalid_Characters()
    {
        // Arrange
        var command = new AddEventCommand
        {
            NewEvent = new NewEventDto
            {
                Name = "Invalid@Name"
            }
        };
        _locationRepositoryMock.Setup(repo => repo.GetByIdAsync(command.NewEvent.LocationId, default))
            .ReturnsAsync(new LocationEntity { Capacity = 50 });
        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(e => e.NewEvent.Name)
            .WithErrorMessage("Name field can contain only alphabetic characters, numbers and '/'");
    }

    [Fact]
    public async Task Should_Have_Error_When_Ticket_Quantity_Exceeds_Location_Capacity()
    {
        // Arrange
        var command = new AddEventCommand
        {
            NewEvent = new NewEventDto
            {
                LocationId = 1,
                TicketDtos = new List<NewTicketDto>
                {
                    new NewTicketDto { Quantity = 100 }
                }
            }
        };

        _locationRepositoryMock.Setup(repo => repo.GetByIdAsync(command.NewEvent.LocationId, default))
            .ReturnsAsync(new LocationEntity { Capacity = 50 });

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(e => e.NewEvent)
            .WithErrorMessage("The total ticket quantity (100) cannot exceed the location capacity (50).");
    }

    [Fact]
    public void Should_Have_Error_When_StartTime_Is_Less_Than_Two_Weeks_In_Advance()
    {
        // Arrange
        var command = new AddEventCommand
        {
            NewEvent = new NewEventDto
            {
                StartTime = DateTime.Now.AddDays(10),
                TicketDtos = new List<NewTicketDto>
                {
                    new NewTicketDto { Quantity = 100 }
                }
            }
        };
        _locationRepositoryMock.Setup(repo => repo.GetByIdAsync(command.NewEvent.LocationId, default))
            .ReturnsAsync(new LocationEntity { Capacity = 50 });
        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(e => e.NewEvent.StartTime)
            .WithErrorMessage("Start time should be given a minimum of 2 weeks in advance");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        // Arrange
        var command = new AddEventCommand
        {
            NewEvent = new NewEventDto
            {
                Name = "Valid Event Name",
                StartTime = DateTime.Now.AddDays(15),
                LocationId = 1,
                TicketDtos = new List<NewTicketDto>
                {
                    new NewTicketDto { Quantity = 125, Price = 15 }
                }
            }
        };

        _locationRepositoryMock.Setup(repo => repo.GetByIdAsync(command.NewEvent.LocationId, default))
            .ReturnsAsync(new LocationEntity { Capacity = 150 });

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}