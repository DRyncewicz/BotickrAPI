using BotickrAPI.Domain.Repositories;
using FluentValidation;

namespace BotickrAPI.Application.Features.Events.Commands.AddEvent;

public class AddEventCommandValidator : AbstractValidator<AddEventCommand>
{
    private readonly ILocationRepository _locationRepository;

    public AddEventCommandValidator(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;

        RuleFor(p => p.NewEvent.Name)
            .MinimumLength(10)
            .WithMessage("Length should be greater than 10 chars")
            .MaximumLength(50)
            .WithMessage("Length should be lower than 50 chars")
            .Matches(@"^[\w/ ]+$")
            .WithMessage("Name field can contain only alphabetic characters, numbers and '/'");

        RuleFor(p => p.NewEvent)
            .Must(e => e.TicketDtos.Sum(t => t.Quantity) <= _locationRepository.GetByIdAsync(e.LocationId, default).Result.Capacity)
            .WithMessage((e, context) =>
            {
                var locationCapacity = _locationRepository.GetByIdAsync(e.NewEvent.LocationId, default).Result.Capacity;
                return $"The total ticket quantity ({e.NewEvent.TicketDtos.Sum(t => t.Quantity)}) cannot exceed the location capacity ({locationCapacity}).";
            });

        RuleFor(p => p.NewEvent.StartTime).GreaterThan(DateTime.Now.AddDays(14))
            .WithMessage("Start time should be given a minimum of 2 weeks in advance");

        RuleForEach(p => p.NewEvent.TicketDtos)
            .SetValidator(new NewTicketDtoValidator());
    }
}