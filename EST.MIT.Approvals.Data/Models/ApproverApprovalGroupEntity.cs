using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class ApproverApprovalGroupEntity : BaseEntity
{
    public int ApproverId { get; init; } = default!;
    public int ApprovalGroupId { get; init; } = default!;

    public ApproverApprovalGroupEntity(int approverId, int approvalGroupId)
    {
        this.ApproverId = approverId;
        this.ApprovalGroupId = approvalGroupId;
    }

    public ApproverApprovalGroupEntity()
    {
    }
}
