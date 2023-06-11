using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories;

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
                LastName = "One,",
                SchemeGrades = new List<SchemeGradeEntity>()
                {
                    new SchemeGradeEntity() { Id = 1 }
                }
            },
            new ApproverEntity()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,",
                SchemeGrades = new List<SchemeGradeEntity>()
                {
                    new SchemeGradeEntity() { Id = 2 }
                }
            },
            new ApproverEntity()
            {
                Id = 3,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,",
                SchemeGrades = new List<SchemeGradeEntity>()
                {
                    new SchemeGradeEntity() { Id = 3 }
                }
            },
        };
        this.Initialise(approvers);
    }
    public async Task<IEnumerable<ApproverEntity>> GetApproversByBySchemeAndGradeAsync(IEnumerable<int> schemeGradeIds)
    {
        var all = await this.GetAllAsync();

        // Return a list of all the approver that have at least one SchemeGrade object
        // in the SchemeGrades collection that matches with the id passed in
        return all.Where(x => x.SchemeGrades.Any(y => schemeGradeIds.Contains(y.Id)));
    }
}
