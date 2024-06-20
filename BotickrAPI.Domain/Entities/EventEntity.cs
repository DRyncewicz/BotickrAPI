using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class EventEntity : AuditableEntity
{
    public string OrganizerId { get; set; } 

    public string Name { get; set; } 

    public string EventType { get; set; } 

    public string ImagePath { get; set; } 

    public string Description { get; set; } 

    public DateTime StartTime { get; set; }

    public TimeSpan Duration { get; set; }

    public string Status { get; set; } 

    public int LocationId { get; set; }

    public LocationEntity Location { get; set; }

    public ICollection<EventReviewEntity> Reviews { get; set; }

    public ICollection<TicketEntity> Tickets { get; set; }

    public ICollection<EventArtistsEntity> EventArtists { get; set; }

    public ICollection<BookingEntity> Bookings { get; set; }
}