using AutoMapper;
using BotickrAPI.Domain.Entities;
using BotickrAPI.Domain.Enums;
using BotickrAPI.Domain.Repositories;
using BotickrAPI.Domain.Transactions;
using MediatR;

namespace BotickrAPI.Application.Features.Events.Commands;

public class AddEventCommandHandler(IEventRepository _eventRepository,
                                    IEventArtistsRepository _eventArtistsRepository,
                                    ITicketRepository _ticketRepository,
                                    IMapper _mapper,
                                    IDatabaseTransaction _databaseTransaction) : IRequestHandler<AddEventCommand, int>
{
    private readonly IEventRepository _eventRepository = _eventRepository;

    private readonly IEventArtistsRepository _eventArtistsRepository = _eventArtistsRepository;

    private readonly ITicketRepository _ticketRepository = _ticketRepository;

    private readonly IDatabaseTransaction _databaseTransaction = _databaseTransaction;

    private readonly IMapper _mapper = _mapper;

    public async Task<int> Handle(AddEventCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _databaseTransaction.BeginAsync(cancellationToken);

        try
        {
            var eventEntity = _mapper.Map<EventEntity>(request.NewEvent);
            eventEntity.OrganizerId = "TODO add from token";
            eventEntity.Status = EventStatus.Waiting.ToString();
            var eventId = await _eventRepository.AddAsync(eventEntity, cancellationToken);

            IList<EventArtistsEntity> eventArtistsEntity = new List<EventArtistsEntity>();
            foreach (int id in request.NewEvent.ArtistIds)
            {
                eventArtistsEntity.Add(new EventArtistsEntity()
                {
                    ArtistId = id,
                    EventId = eventId
                });
            }

            var isSuccess = await _eventArtistsRepository.AddRangeAsync(eventArtistsEntity, cancellationToken);
            if (!isSuccess) throw new Exception();

            var tickets = _mapper.Map<IEnumerable<TicketEntity>>(request.NewEvent.TicketDtos).ToList();
            tickets.ForEach(t => t.EventId = eventId);

            await _ticketRepository.AddRangeAsync(tickets, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return eventId;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw new Exception();
        }
    }
}
