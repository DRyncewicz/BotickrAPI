namespace BotickrAPI.Application.Dtos.Tickets;

public class NewTicketDto
{
    public double Price { get; set; }

    public int Quantity { get; set; }

    public string TicketType { get; set; } = string.Empty;
}
