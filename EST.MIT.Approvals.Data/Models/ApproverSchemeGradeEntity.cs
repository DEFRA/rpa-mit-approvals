using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class ApproverSchemeGradeEntity : BaseEntity
{
    public int ApproverId { get; init; }

    public int SchemeGradeId { get; init; }

    public ApproverEntity Approver { get; set; } = default!;

    public SchemeGradeEntity SchemeGrade { get; set; } = default!;
}
