using Approvals.Api.Data.Entities;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface IApproverRepository : IRepository<ApproverEntity>
{
    Task<IEnumerable<ApproverEntity>> GetApproversByIdsAsync(IEnumerable<int> ids);
}
