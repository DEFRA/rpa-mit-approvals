using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class GradeEntity : BaseEntity
{
    public string Code { get; init; } = default!;

    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;
}
