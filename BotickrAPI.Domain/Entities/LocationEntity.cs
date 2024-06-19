using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class LocationEntity : AuditableEntity
{
    public string City { get; set; } 

    public string Venue { get; set; } 

    public int Capacity { get; set; }

    public ICollection<EventEntity> Events { get; set; } 
}