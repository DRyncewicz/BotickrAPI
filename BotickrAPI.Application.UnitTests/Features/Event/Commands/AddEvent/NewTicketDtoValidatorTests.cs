using BotickrAPI.Application.Dtos.Tickets;
using BotickrAPI.Application.Features.Events.Commands.AddEvent;
using FluentValidation.TestHelper;
using Xunit;

namespace BotickrAPI.Application.UnitTests.Features.Event.Commands.AddEvent;

public class NewTicketDtoValidatorTests
{
    private readonly NewTicketDtoValidator _validator;

    public NewTicketDtoValidatorTests()
    {
        _validator = new NewTicketDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Price_Is_Less_Than_15()
    {
        // Arrange
        var ticket = new NewTicketDto
        {
            Price = 10,
            Quantity = 150
        };

        // Act
        var result = _validator.TestValidate(ticket);

        // Assert
        result.ShouldHaveValidationErrorFor(t => t.Price)
            .WithErrorMessage("Prices for standard event organizers must be in the range of 15 - 200 PLN");
    }

    [Fact]
    public void Should_Have_Error_When_Price_Is_Greater_Than_200()
    {
        // Arrange
        var ticket = new NewTicketDto
        {
            Price = 250,
            Quantity = 150
        };

        // Act
        var result = _validator.TestValidate(ticket);

        // Assert
        result.ShouldHaveValidationErrorFor(t => t.Price)
            .WithErrorMessage("Prices for standard event organizers must be in the range of 15 - 200 PLN");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Price_Is_In_Valid_Range()
    {
        // Arrange
        var ticket = new NewTicketDto
        {
            Price = 100,
            Quantity = 150
        };

        // Act
        var result = _validator.TestValidate(ticket);

        // Assert
        result.ShouldNotHaveValidationErrorFor(t => t.Price);
    }

    [Fact]
    public void Should_Have_Error_When_Quantity_Is_Less_Than_100()
    {
        // Arrange
        var ticket = new NewTicketDto
        {
            Price = 100,
            Quantity = 50
        };

        // Act
        var result = _validator.TestValidate(ticket);

        // Assert
        result.ShouldHaveValidationErrorFor(t => t.Quantity)
            .WithErrorMessage("Minimal amount of ticket register for single organizer should be grater than 100");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Quantity_Is_Greater_Than_100()
    {
        // Arrange
        var ticket = new NewTicketDto
        {
            Price = 100,
            Quantity = 150
        };

        // Act
        var result = _validator.TestValidate(ticket);

        // Assert
        result.ShouldNotHaveValidationErrorFor(t => t.Quantity);
    }
}