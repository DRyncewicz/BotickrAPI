using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class TicketEntity : AuditableEntity
{
    public int EventId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public string TicketType { get; set; }

    public EventEntity Event { get; set; }

    public ICollection<BookingDetailEntity> Bookings { get; set; }
}