using Approvals.Api.Data.Entities;

namespace EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

public interface ISchemeRepository : IRepository<SchemeEntity>
{
    Task<SchemeEntity?> GetByCodeAsync(string code);
}
