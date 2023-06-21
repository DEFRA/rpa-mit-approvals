using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories;

public class GradeRepository : Repository<GradeEntity>, IGradeRepository
{

    public GradeRepository(ApprovalsContext context)
        : base(context)
    {
    }
}
