using AutoMapper;
using BotickrAPI.Application.Dtos.Events;
using BotickrAPI.Application.Dtos.Tickets;
using BotickrAPI.Domain.Exceptions;
using BotickrAPI.Domain.Repositories;
using MediatR;

namespace BotickrAPI.Application.Features.Events.Queries.GetEventDetailsById
{
    public class GetEventDetailsByIdQueryHandler(IMapper _mapper, IEventRepository _eventRepository, ITicketRepository _ticketRepository) : IRequestHandler<GetEventDetailsByIdQuery, DetailEventInfoDto>
    {
        private readonly IEventRepository _eventRepository = _eventRepository;

        private readonly IMapper _mapper = _mapper;

        private readonly ITicketRepository _ticketRepository = _ticketRepository;


        public async Task<DetailEventInfoDto> Handle(GetEventDetailsByIdQuery request, CancellationToken cancellationToken)
        {

            var events = await _eventRepository.GetByIdAsync(request.Id, cancellationToken);
            if (events == null)
            {
                throw new NotFoundException();
            }
            var eventsDto = _mapper.Map<DetailEventInfoDto>(events);

            var tickets = await _ticketRepository.GetTicketsByEventIdAsync(request.Id, cancellationToken);
            if (!tickets.Any())
            {
                throw new NotFoundException();
            }
            var ticketDto = _mapper.Map<IEnumerable<TicketDto>>(tickets);

            eventsDto.Tickets = ticketDto;

            return eventsDto;
        }
    }
}
