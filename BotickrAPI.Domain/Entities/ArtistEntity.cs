using BotickrAPI.Domain.Entities.Common;

namespace BotickrAPI.Domain.Entities;

public class ArtistEntity : AuditableEntity
{
    public string Name { get; set; } 

    public string Surname { get; set; } 

    public string ImagePath { get; set; }

    public int? Age { get; set; }

    public string? ArtName { get; set; }

    public string? BirthCity { get; set; }

    public string Discipline { get; set; }

    public string Description { get; set; }

    public int Likes { get; set; } = 0;

    public ICollection<TicketEntity> Tickets { get; set; } 

    public ICollection<EventArtistsEntity> EventArtists { get; set; }
}
