﻿using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface IApproverRepository : IRepository<ApproverEntity>
{
    Task<IEnumerable<ApproverEntity>> GetApproversBySchemeAndGradeAsync(IEnumerable<int> schemeGradeIds);
}
