namespace BotickrAPI.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(Exception innerException, string message)
        : base(message, innerException)
    {
    }

    public NotFoundException(string name, object key)
        : base($"Record \"{name}\" ({key}) was not found.")
    {
    }
}
