using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface ISchemeGradeApprovalRepository : IRepository<SchemeGradeApprovalEntity>
{
    Task<IEnumerable<SchemeGradeApprovalEntity>> GetAllBySchemeGradesBySchemeAndApprovalLimit(int schemeId, decimal approvalLimit);
}
