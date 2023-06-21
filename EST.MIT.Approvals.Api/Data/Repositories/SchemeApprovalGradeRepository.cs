using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Data.Repositories;

public class SchemeApprovalGradeRepository : Repository<SchemeApprovalGradeEntity>, ISchemeApprovalGradeRepository
{

    public SchemeApprovalGradeRepository(ApprovalsContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<SchemeApprovalGradeEntity>> GetAllBySchemeAndApprovalLimit(int schemeId, decimal approvalLimit)
    {
        return await this.Context.SchemeApprovalGrades.Where(x => x.SchemeGrade.Scheme.Id == schemeId && (x.ApprovalLimit >= approvalLimit || x.IsUnlimited)).ToListAsync();
    }
}
