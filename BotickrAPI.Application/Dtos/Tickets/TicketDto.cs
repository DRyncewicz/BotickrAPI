namespace BotickrAPI.Application.Dtos.Tickets;

public class TicketDto
{
    public int Id { get; set; } 

    public int EventId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public string TicketType { get; set; } = string.Empty;
}
