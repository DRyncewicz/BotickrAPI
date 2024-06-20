using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Persistence.Configurations;

public static class BookingEntityConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        SetBooking(modelBuilder.Entity<BookingEntity>());
    }
    public static void SetBooking(EntityTypeBuilder<BookingEntity> builder)
    {
        builder.Property(p => p.BookingTime).IsRequired();
        builder.Property(p => p.TotalPrice).IsRequired();
        builder.Property(p => p.Status).IsRequired();

        builder.HasOne(p => p.Event)
            .WithMany(p => p.Bookings)
            .HasForeignKey(p => p.EventId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}