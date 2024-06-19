using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Persistence.Configurations;

public static class EventReviewEntityConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        SetEventReview(modelBuilder.Entity<EventReviewEntity>());
    }
    public static void SetEventReview(EntityTypeBuilder<EventReviewEntity> builder)
    {
        builder.Property(p => p.EventId).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.Rating).IsRequired();
        builder.Property(p => p.Description).IsRequired();
    }
}
