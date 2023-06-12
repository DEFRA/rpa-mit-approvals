using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class SchemeGradeEntity : BaseEntity
{
    public SchemeEntity Scheme { get; set; } = default!;

    public GradeEntity Grade { get; set; } = default!;
}
