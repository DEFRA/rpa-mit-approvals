using Approvals.Api.Data.Entities;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

namespace Approvals.Api.Data.Repositories;

public class SchemeGradeApproverRepository : Repository<SchemeGradeApproverEntity>, ISchemeGradeApproverRepository
{

    public SchemeGradeApproverRepository()
        :base()
    {
        var grades = new List<SchemeGradeApproverEntity>()
        {
            new SchemeGradeApproverEntity()
            {
                Id = 1,
                SchemeId = 1,
                GradeId = 1,
                ApproverId = 1,
            },
        };
        this.Initialise(grades);
    }

    public async Task<IEnumerable<SchemeGradeApproverEntity>> GetAllBySchemeAndGrade(int schemeId, int gradeId)
    {
        var all = await this.GetAllAsync();

        return all.Where(x => x.SchemeId == schemeId && x.GradeId == gradeId);
    }
}
