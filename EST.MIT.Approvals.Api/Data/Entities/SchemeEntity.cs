namespace Approvals.Api.Data.Entities;

public class SchemeEntity : BaseEntity
{
    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;

    public string Code { get; init; } = default!;
}
