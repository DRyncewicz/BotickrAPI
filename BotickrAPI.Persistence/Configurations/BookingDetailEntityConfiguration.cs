using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Persistence.Configurations;

public class BookingDetailEntityConfiguration : IEntityTypeConfiguration<BookingDetailEntity>
{
    public void Configure(EntityTypeBuilder<BookingDetailEntity> builder)
    {
        builder.Property(p => p.BookingId).IsRequired();
        builder.Property(p => p.TicketId).IsRequired();
        builder.Property(p => p.Quantity).IsRequired();
    }
}