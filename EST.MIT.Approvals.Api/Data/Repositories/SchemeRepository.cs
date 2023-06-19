using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Data.Repositories;

public class SchemeRepository : Repository<SchemeEntity>, ISchemeRepository
{
    public SchemeRepository(ApprovalsContext context)
        : base(context)
    {
    }

    public async Task<SchemeEntity?> GetByCodeAsync(string code)
    {
        return await this.Context.Schemes.FirstOrDefaultAsync(x => x.Code.ToLower().Trim() == code.ToLower().Trim());
    }
}
