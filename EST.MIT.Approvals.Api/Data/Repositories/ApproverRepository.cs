using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Data.Repositories;

public class ApproverRepository : Repository<ApproverEntity>, IApproverRepository
{
    public ApproverRepository(ApprovalsContext context)
        : base(context)
    {
    }

    public async Task<ApproverEntity?> GetApproverByEmailAddressAndSchemeAsync(string approverEmailAddress, string schemeCode)
    {
        return await this.Context.Approvers
            .Include(x => x.Schemes)
            .FirstOrDefaultAsync(x =>
                x.EmailAddress.ToLower().Trim() == approverEmailAddress.ToLower().Trim() &&
                x.Schemes.Any(y => string.Equals(y.Code, schemeCode, StringComparison.CurrentCultureIgnoreCase)));
    }
}
