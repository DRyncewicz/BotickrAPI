using AutoMapper;
using BotickrAPI.Application.MapperProfiles.Artists;
using BotickrAPI.Application.MapperProfiles.Events;
using BotickrAPI.Application.MapperProfiles.Tickets;

namespace BotickrAPI.Application.UnitTests.MapperProfiles;

public class MappingTestFixture
{
    public IConfigurationProvider ConfigurationProvider { get; set; }

    public IMapper Mapper { get; set; }

    public MappingTestFixture()
    {
        ConfigurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ArtistProfile>();
            cfg.AddProfile<EventProfile>();
            cfg.AddProfile<TicketProfile>();
        });

        Mapper = ConfigurationProvider.CreateMapper();
    }
}