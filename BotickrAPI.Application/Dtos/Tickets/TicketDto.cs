namespace BotickrAPI.Application.Dtos.Tickets;

public class TicketDto
{
    public double Price { get; set; }

    public int TotalQuantity { get; set; }

    public int AvailableQuantity { get; set; }

    public string TicketType { get; set; } = string.Empty;

    public bool IsSoldOut { get; set; }
}
