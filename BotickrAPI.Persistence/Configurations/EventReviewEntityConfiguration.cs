using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Persistence.Configurations;

public class EventReviewEntityConfiguration : IEntityTypeConfiguration<EventReviewEntity>
{
    public void Configure(EntityTypeBuilder<EventReviewEntity> builder)
    {
        builder.Property(p => p.EventId).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.Rating).IsRequired();
        builder.Property(p => p.Description).IsRequired();
    }
}
