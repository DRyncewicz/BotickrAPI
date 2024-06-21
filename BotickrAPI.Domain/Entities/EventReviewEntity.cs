using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class EventReviewEntity : AuditableEntity
{
    public int EventId { get; set; }

    public string UserId { get; set; } 

    public int Rating { get; set; }

    public string Description { get; set; } 

    public EventEntity Event { get; set; } 
}