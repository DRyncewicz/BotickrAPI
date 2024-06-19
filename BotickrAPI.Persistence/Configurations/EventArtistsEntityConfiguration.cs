using BotickrAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BotickrAPI.Persistence.Configurations;

public class EventArtistsEntityConfiguration : IEntityTypeConfiguration<EventArtistsEntity>
{
    public void Configure(EntityTypeBuilder<EventArtistsEntity> builder)
    {
        builder.HasKey(ea => new { ea.ArtistId, ea.EventId });
        builder.Property(p => p.ArtistId).IsRequired();
        builder.Property(p => p.EventId).IsRequired();  
    }
}
