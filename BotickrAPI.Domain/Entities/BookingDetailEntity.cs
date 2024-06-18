using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class BookingDetailEntity : AuditableEntity
{
    public int BookingId { get; set; }

    public int TicketId { get; set; }

    public int Quantity { get; set; }

    public BookingEntity Booking { get; set; } = new BookingEntity();

    public TicketEntity Ticket { get; set; } = new TicketEntity();
}