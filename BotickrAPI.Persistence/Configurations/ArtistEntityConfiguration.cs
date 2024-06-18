using BotickrAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BotickrAPI.Persistence.Configurations;

public class ArtistEntityConfiguration : IEntityTypeConfiguration<ArtistEntity>
{
    public void Configure(EntityTypeBuilder<ArtistEntity> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Surname).IsRequired();
        builder.Property(p => p.Discipline).IsRequired();
        builder.Property(p => p.Description).IsRequired();
    }
}
