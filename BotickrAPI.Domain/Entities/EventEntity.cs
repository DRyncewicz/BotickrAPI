using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class EventEntity : AuditableEntity
{
    public string OrganizerId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string EventType { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public TimeSpan Duration { get; set; }

    public string Status { get; set; } = string.Empty;

    public int LocationId { get; set; }

    public LocationEntity Location { get; set; } = new LocationEntity();

    public ICollection<EventReviewEntity> Reviews { get; set; } = [];

    public ICollection<TicketEntity> Ticket { get; set; } = [];

    public ICollection<ArtistEntity> Artists { get; set; } = [];
}