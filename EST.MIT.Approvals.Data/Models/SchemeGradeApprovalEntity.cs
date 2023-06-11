using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class SchemeGradeApprovalEntity : BaseEntity
{
    public SchemeGradeEntity SchemeGrade { get; set; } = default!;

    public decimal ApprovalLimit { get; init; } = default!;

    public bool IsUnlimited { get; init; } = default!;
}
