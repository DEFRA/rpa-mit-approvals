using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class SchemeGradeEntity : BaseEntity
{
    public int SchemeId { get; init; }

    public int GradeId { get; init; }

    public SchemeEntity Scheme { get; set; } = default!;

    public GradeEntity Grade { get; set; } = default!;
}
