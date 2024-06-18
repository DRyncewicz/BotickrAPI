using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Persistence.Configurations;

public class LocationEntityConfiguration : IEntityTypeConfiguration<LocationEntity>
{
    public void Configure(EntityTypeBuilder<LocationEntity> builder)
    {
        builder.Property(p => p.City).IsRequired();
        builder.Property(p => p.Venue).IsRequired();
        builder.Property(p => p.Capacity).IsRequired();
    }
}
