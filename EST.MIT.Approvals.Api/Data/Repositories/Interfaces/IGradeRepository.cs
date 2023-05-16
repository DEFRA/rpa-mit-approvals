using Approvals.Api.Data.Entities;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface IGradeRepository : IRepository<GradeEntity>
{
    Task<GradeEntity?> GetByApprovalLimit(decimal approvalLimit);
}
