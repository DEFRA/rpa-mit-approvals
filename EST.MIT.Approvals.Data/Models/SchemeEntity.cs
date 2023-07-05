using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class SchemeEntity : BaseEntity
{
    public string Code { get; init; } = default!;

    public List<ApproverEntity> Approvers { get; set; } = new();

    public SchemeEntity(string code)
    {
        this.Code = code;
    }

    public SchemeEntity()
    {
    }
}
