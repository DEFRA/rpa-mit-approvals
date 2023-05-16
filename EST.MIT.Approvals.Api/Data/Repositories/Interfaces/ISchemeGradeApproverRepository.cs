using Approvals.Api.Data.Entities;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface ISchemeGradeApproverRepository : IRepository<SchemeGradeApproverEntity>
{
    Task<IEnumerable<SchemeGradeApproverEntity>> GetAllBySchemeAndGrade(int schemeId, int gradeId);
}
