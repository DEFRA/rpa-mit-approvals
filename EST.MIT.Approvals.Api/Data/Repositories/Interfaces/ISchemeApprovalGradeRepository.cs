using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface ISchemeApprovalGradeRepository : IRepository<SchemeApprovalGradeEntity>
{
    Task<IEnumerable<SchemeApprovalGradeEntity>> GetAllBySchemeAndApprovalLimit(int schemeId, decimal approvalLimit);
}
