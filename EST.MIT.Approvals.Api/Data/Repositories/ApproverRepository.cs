using Approvals.Api.Data.Entities;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

namespace Approvals.Api.Data.Repositories;

public class ApproverRepository : Repository<ApproverEntity>, IApproverRepository
{
    public ApproverRepository()
        : base()
    {
        var approvers = new List<ApproverEntity>()
        {
            new ApproverEntity()
            {
                Id = 1,
                EmailAddress = "ApproverOne@defra.gov.uk",
                FirstName = "Approver",
                LastName = "One,"
            },
            new ApproverEntity()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,"
            },
            new ApproverEntity()
            {
                Id = 3,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,"
            }
        };
        this.Initialise(approvers);
    }
    public async Task<IEnumerable<ApproverEntity>> GetApproversByIdsAsync(IEnumerable<int> ids)
    {
        var all = await this.GetAllAsync();

        return all.Where(x => ids.Contains(x.Id));
    }
}
