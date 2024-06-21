using BotickrAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BotickrAPI.Persistence.Configurations;

public static class ArtistEntityConfiguration 
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        SetArtist(modelBuilder.Entity<ArtistEntity>());
    }
    public static void SetArtist(EntityTypeBuilder<ArtistEntity> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Surname).IsRequired();
        builder.Property(p => p.Discipline).IsRequired();
        builder.Property(p => p.Description).IsRequired();
    }
}
