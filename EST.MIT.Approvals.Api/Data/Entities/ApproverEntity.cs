namespace Approvals.Api.Data.Entities;

public class ApproverEntity : BaseEntity
{
    public string EmailAddress { get; init; } = default!;

    public string FirstName { get; init; } = default!;

    public string LastName { get; init; } = default!;
}
