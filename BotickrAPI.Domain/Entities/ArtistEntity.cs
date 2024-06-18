using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class ArtistEntity : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public int? Age { get; set; }

    public string? ArtName { get; set; }

    public string? BirthCity { get; set; }

    public string Discipline { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Likes { get; set; } = 0;

    public ICollection<TicketEntity> Tickets { get; set; } = [];

    public ICollection<EventEntity> Events { get; set; } = [];
}
