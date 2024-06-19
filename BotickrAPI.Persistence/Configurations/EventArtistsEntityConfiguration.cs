using BotickrAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BotickrAPI.Persistence.Configurations;

public static class EventArtistsEntityConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        SetEventArtists(modelBuilder.Entity<EventArtistsEntity>());
    }
    public static void SetEventArtists(EntityTypeBuilder<EventArtistsEntity> builder)
    {
        builder.HasKey(ea => new { ea.ArtistId, ea.EventId });
        builder.Property(p => p.ArtistId).IsRequired();
        builder.Property(p => p.EventId).IsRequired();

        builder.HasOne(p => p.Event)
            .WithMany(p => p.EventArtists)
            .HasForeignKey(p => p.EventId);

        builder.HasOne(e => e.Artist)
            .WithMany(p => p.EventArtists)
            .HasForeignKey(e => e.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
