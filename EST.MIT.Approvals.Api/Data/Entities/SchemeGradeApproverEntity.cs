namespace Approvals.Api.Data.Entities;

public class SchemeGradeApproverEntity : BaseEntity
{
    public int SchemeId { get; init; } = default!;

    public int GradeId { get; init; } = default!;

    public int ApproverId { get; init; } = default!;
}
