using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories;

public class SchemeGradeApprovalRepository : Repository<SchemeGradeApprovalEntity>, ISchemeGradeApprovalRepository
{

    public SchemeGradeApprovalRepository()
        : base()
    {
        var entities = new List<SchemeGradeApprovalEntity>()
        {
            new SchemeGradeApprovalEntity()
            {
                Id = 1,
                SchemeGrade = new SchemeGradeEntity()
                {
                    Id = 1,
                    Scheme = new SchemeEntity()
                    {
                        Id = 1,
                        Code = "S1",
                        Name = "Scheme 1",
                        Description = "This is the description for Scheme 1",
                    },
                    Grade = new GradeEntity()
                    {
                        Id = 1,
                        Code = "G1",
                        Name = "Grade 1",
                        Description = "This is the description for Grade 1",
                    },
                },
                ApprovalLimit = 1000M,
                IsUnlimited = false,
            },
        };
        this.Initialise(entities);
    }

    public async Task<IEnumerable<SchemeGradeApprovalEntity>> GetAllBySchemeGradesBySchemeAndApprovalLimit(int schemeId, decimal approvalLimit)
    {
        var all = await this.GetAllAsync();

        return all.Where(x => x.SchemeGrade.Scheme.Id == schemeId && (x.ApprovalLimit >= approvalLimit || x.IsUnlimited));
    }
}
