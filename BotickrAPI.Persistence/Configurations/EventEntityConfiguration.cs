using BotickrAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BotickrAPI.Persistence.Configurations;

public static class EventEntityConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        SetEvent(modelBuilder.Entity<EventEntity>());
    }
    public static void SetEvent(EntityTypeBuilder<EventEntity> builder)
    {
        builder.Property(p => p.OrganizerId).IsRequired();
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.EventType).IsRequired();
        builder.Property(p => p.ImagePath).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.StartTime).IsRequired();
        builder.Property(p => p.Duration).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.LocationId).IsRequired().HasColumnName("LocationId");

        builder.HasOne(e => e.Location)
            .WithMany(f => f.Events)
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
