using BotickrAPI.Application.Dtos.Events;
using MediatR;

namespace BotickrAPI.Application.Features.Events.Queries.GetEventsByFilters
{
    public class GetEventsByFiltersQuery : IRequest<IEnumerable<EventDto>>
    {
        public int? LocationId { get; set; }

        public string? SearchString { get; set; }    

        public DateTime? EventStart { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
