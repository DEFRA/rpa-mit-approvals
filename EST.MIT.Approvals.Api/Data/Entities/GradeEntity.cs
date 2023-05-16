namespace Approvals.Api.Data.Entities;

public class GradeEntity : BaseEntity
{
    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public decimal ApprovalLimit { get; init; } = default!;
}
