using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Data.Repositories;

public class ApproverRepository : Repository<ApproverEntity>, IApproverRepository
{
    public ApproverRepository(ApprovalsContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<ApproverEntity>> GetApproversBySchemeAndGradeAsync(IEnumerable<int> schemeGradeIds)
    {
        return await this.Context.Approvers
            .Include(x => x.SchemeGrades)
            .Where(x => x.SchemeGrades.Any(y => schemeGradeIds.Contains(y.Id))).ToListAsync();
    }
}
