using BotickrAPI.Application.Dtos.Artists;
using BotickrAPI.Application.Helpers;
using System.Text.Json.Serialization;

namespace BotickrAPI.Application.Dtos.Events;

public class EventDto
{
    public string Name { get; set; } = string.Empty;

    public string EventType { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    [JsonConverter(typeof(TimeSpanConverter))]
    public TimeSpan Duration { get; set; }

    public int LocationId { get; set; }

    public IEnumerable<ArtistDto> Artists { get; set; } = [];

}
