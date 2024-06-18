using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class EventEntity : AuditableEntity
{
    public string OrganizerEmail { get; set; }

    public string Name { get; set; }

    public string EventType { get; set; }

    public string ImagePath { get; set; }

    public string Description { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string Status { get; set; }

    public int LocationId { get; set; }

    public LocationEntity Location { get; set; }

    public ICollection<EventReviewEntity> Reviews { get; set; }

    public ICollection<TicketEntity> Ticket { get; set; }

    public ICollection<ArtistEntity> Artists { get; set; }
}