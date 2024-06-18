namespace BotickrAPI.Application.Dtos.Locations;

public class LocationDto
{
    public int LocationId { get; set; }

    public string City { get; set; } = string.Empty;

    public string Venue { get; set; } = string.Empty;

    public int Capacity { get; set; }
}
