using AutoMapper;
using Xunit;

namespace BotickrAPI.Application.UnitTests.MapperProfiles;

public class MappingTest : IClassFixture<MappingTestFixture>
{
    private readonly IConfigurationProvider _configuration;


    public MappingTest(MappingTestFixture fixture)
    {
        _configuration = fixture.ConfigurationProvider;
    }

    [Fact]
    public void ShouldHaveConfigurationIsValid()
    {
        _configuration.AssertConfigurationIsValid();
    }
}