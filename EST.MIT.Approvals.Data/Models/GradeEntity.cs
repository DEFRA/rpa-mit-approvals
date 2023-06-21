using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class GradeEntity : BaseEntity
{
    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public string Code { get; init; } = default!;

}
