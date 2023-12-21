using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Data.Repositories;

public class ApprovalGroupRepository : Repository<ApprovalGroupEntity>, IApprovalGroupRepository
{
    public ApprovalGroupRepository(ApprovalsContext context)
        : base(context)
    {
    }

    public async Task<ApprovalGroupEntity?> GetByCodeAsync(string code)
    {
        return await this.Context.ApprovalGroups.FirstOrDefaultAsync(x => x.Code.ToLower().Trim() == code.ToLower().Trim());
    }
}
