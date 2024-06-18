using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class BookingEntity : AuditableEntity
{
    public string UserId { get; set; }

    public int EventId { get; set; }

    public double TotalPrice { get; set; }

    public DateTime BookingTime { get; set; }

    public string Status { get; set; }
}