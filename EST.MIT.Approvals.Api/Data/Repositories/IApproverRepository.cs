using Approvals.Api.Data.Entities;

namespace Approvals.Api.Data.Repositories;

public interface IApproverRepository
{
    Task<IEnumerable<ApproverEntity>> GetApproversByGradeAsync(int grade);
}
