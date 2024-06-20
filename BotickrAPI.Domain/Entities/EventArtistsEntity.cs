namespace BotickrAPI.Domain.Entities;

public class EventArtistsEntity
{
    public int ArtistId { get; set; }

    public int EventId { get; set; }

    public EventEntity Event { get; set; } = new();

    public ArtistEntity Artist { get; set; } = new();
}
