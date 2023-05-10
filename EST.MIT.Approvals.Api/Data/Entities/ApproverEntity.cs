namespace Approvals.Api.Data.Entities;

public class ApproverEntity
{
    public int Id { get; init; } = default!;

    public string EmailAddress { get; init; } = default!;

    public string FirstName { get; init; } = default!;

    public string LastName { get; init; } = default!;
}
