using BotickrAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BotickrAPI.Persistence.Seeds;

public static class LocationEntityDataSeed
{
    public static void LocationDataSeed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LocationEntity>().HasData(
            new LocationEntity
            {
                Capacity = 3800,
                City = "Koszalin",
                Venue = "Hala Widowiskowo-Sportowa",
                CreatedBy = "System",
                Created = DateTime.MinValue,
                StatusId = 1,
                Id = 1
            },
            new LocationEntity
            {
                Capacity = 80000,
                City = "Warszawa",
                Venue = "PGE Narodowy",
                CreatedBy = "System",
                Created = DateTime.MinValue,
                StatusId = 1,
                Id = 2
            },
            new LocationEntity
            {
                Capacity = 15328,
                City = "Gdańsk",
                Venue = "Stadion Energa Gdańsk",
                CreatedBy = "System",
                Created = DateTime.MinValue,
                StatusId = 1,
                Id = 3
            },
            new LocationEntity
            {
                Capacity = 18000,
                City = "Kraków",
                Venue = "Tauron Arena Kraków",
                CreatedBy = "System",
                Created = DateTime.MinValue,
                StatusId = 1,
                Id = 4
            },
            new LocationEntity
            {
                Capacity = 7500,
                City = "Wrocław",
                Venue = "Hala Stulecia",
                CreatedBy = "System",
                Created = DateTime.MinValue,
                StatusId = 1,
                Id = 5
            },
            new LocationEntity
            {
                Capacity = 5000,
                City = "Poznań",
                Venue = "Hala Arena",
                CreatedBy = "System",
                Created = DateTime.MinValue,
                StatusId = 1,
                Id = 6
            }
            );
    }
}
