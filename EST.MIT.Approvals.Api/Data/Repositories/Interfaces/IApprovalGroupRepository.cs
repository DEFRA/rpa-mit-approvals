using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface IApprovalGroupRepository : IRepository<ApprovalGroupEntity>
{
    Task<ApprovalGroupEntity?> GetByCodeAsync(string code);
}
