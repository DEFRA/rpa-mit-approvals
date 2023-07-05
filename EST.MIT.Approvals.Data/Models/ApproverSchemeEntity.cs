using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class ApproverSchemeEntity : BaseEntity
{
    public int ApproverId { get; init; } = default!;
    public int SchemeId { get; init; } = default!;

    public ApproverSchemeEntity(int approverId, int schemeId)
    {
        this.ApproverId = approverId;
        this.SchemeId = schemeId;
    }

    public ApproverSchemeEntity()
    {
    }
}
