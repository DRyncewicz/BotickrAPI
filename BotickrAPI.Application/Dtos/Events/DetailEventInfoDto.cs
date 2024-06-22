using BotickrAPI.Application.Dtos.Tickets;
using BotickrAPI.Application.Helpers;
using System.Text.Json.Serialization;

namespace BotickrAPI.Application.Dtos.Events;

public class DetailEventInfoDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string EventType { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan Duration { get; set; }

    public string Status { get; set; } = string.Empty;

    public int LocationId { get; set; }

    public IEnumerable<TicketDto> Tickets { get; set; } = [];
}
