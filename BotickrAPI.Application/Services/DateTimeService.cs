using BotickrAPI.Application.Abstractions.Services;

namespace BotickrAPI.Application.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;

}
