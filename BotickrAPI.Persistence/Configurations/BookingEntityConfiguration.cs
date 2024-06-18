using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Persistence.Configurations;

public class BookingEntityConfiguration : IEntityTypeConfiguration<BookingEntity>
{
    public void Configure(EntityTypeBuilder<BookingEntity> builder)
    {
        builder.Property(p => p.BookingTime).IsRequired();
        builder.Property(p => p.TotalPrice).IsRequired();
        builder.Property(p => p.Status).IsRequired();
    }
}