using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Persistence.Configurations;

public static class BookingDetailEntityConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        SetBookingDetail(modelBuilder.Entity<BookingDetailEntity>());
    }
    public static void SetBookingDetail(EntityTypeBuilder<BookingDetailEntity> builder)
    {
        builder.Property(p => p.BookingId).IsRequired();
        builder.Property(p => p.TicketId).IsRequired();
        builder.Property(p => p.Quantity).IsRequired();
    }
}