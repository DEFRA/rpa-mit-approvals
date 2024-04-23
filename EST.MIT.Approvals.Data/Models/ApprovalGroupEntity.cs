using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class ApprovalGroupEntity : BaseEntity
{
    public string Code { get; init; } = default!;

    public List<ApproverEntity> Approvers { get; set; } = new();

    public ApprovalGroupEntity(string code)
    {
        this.Code = code;
    }

    public ApprovalGroupEntity()
    {
    }
}
