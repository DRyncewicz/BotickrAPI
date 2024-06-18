namespace BotickrAPI.Domain.Entities.Common;

public class AuditableEntity
{
    public int Id { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? Modified { get; set; }

    public int StatusId { get; set; } = 1;

    public string? InactivatedBy { get; set; }

    public DateTime? Inactivated { get; set; }

}
