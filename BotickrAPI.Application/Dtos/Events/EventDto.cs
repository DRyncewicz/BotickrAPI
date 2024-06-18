using BotickrAPI.Application.Dtos.Tickets;

namespace BotickrAPI.Application.Dtos.Events;

public class EventDto
{
    public string Name { get; set; } = string.Empty;

    public string EventType { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public TimeSpan Duration { get; set; }

    public int LocationId { get; set; }

    public IEnumerable<int> ArtistIds { get; set; } = [];

    public TicketDto TicketDto { get; set; } = new();
}
