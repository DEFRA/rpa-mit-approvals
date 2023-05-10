using Approvals.Api.Data.Entities;

namespace Approvals.Api.Data.Repositories;

public class ApproverRepository : IApproverRepository
{
    public async Task<IEnumerable<ApproverEntity>> GetApproversByGradeAsync(int grade)
    {
        return await Task.Run(() => new List<ApproverEntity>()
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
        });
    }
}
