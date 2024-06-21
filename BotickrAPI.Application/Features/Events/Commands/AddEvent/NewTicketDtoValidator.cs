using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Application.Dtos.Tickets;
using FluentValidation;

namespace BotickrAPI.Application.Features.Events.Commands.AddEvent;

public class NewTicketDtoValidator : AbstractValidator<NewTicketDto>
{
    public NewTicketDtoValidator()
    {
        RuleFor(p => p.Price).InclusiveBetween(15, 200)
            .WithMessage("Prices for standard event organizers must be in the range of 15 - 200 PLN");

        RuleFor(p => p.Quantity).GreaterThan(100)
            .WithMessage("Minimal amount of ticket register for single organizer should be grater than 100");
    }
}
