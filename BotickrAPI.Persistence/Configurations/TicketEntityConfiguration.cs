using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BotickrAPI.Domain.Entities;

namespace BotickrAPI.Persistence.Configurations;

public static class TicketEntityConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        SetTicket(modelBuilder.Entity<TicketEntity>());
    }
    public static void SetTicket(EntityTypeBuilder<TicketEntity> builder)
    {
        builder.Property(p => p.Quantity).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p=>p.TicketType).IsRequired();
        builder.HasOne(p => p.Event)
            .WithMany(p => p.Tickets)
            .HasForeignKey(p => p.EventId);
    }
}