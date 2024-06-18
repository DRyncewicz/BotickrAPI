﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BotickrAPI.Domain.Entities.Common;
using BotickrAPI.Domain.Entities;
using BotickrAPI.Application.Abstractions.Services;

namespace BotickrAPI.Persistence.DbContext
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IDateTimeService _dateTime;

        public DbSet<BookingEntity> Bookings { get; set; }

        public DbSet<BookingDetailEntity> BookingDetails { get; set; }

        public DbSet<TicketEntity> Tickets { get; set; }

        public DbSet<EventEntity> Events { get; set; }

        public DbSet<EventReviewEntity> EventReviews { get; set; }

        public DbSet<ArtistEntity> Artists { get; set; }

        public DbSet<LocationEntity> Locations { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IDateTimeService dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = string.Empty;
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.StatusId = 1;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = _dateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.ModifiedBy = string.Empty;
                        entry.Entity.Modified = _dateTime.Now;
                        entry.Entity.Inactivated = _dateTime.Now;
                        entry.Entity.InactivatedBy = string.Empty;
                        entry.Entity.StatusId = 0;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}