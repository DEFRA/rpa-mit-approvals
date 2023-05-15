using Approvals.Api.Data.Entities;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

namespace Approvals.Api.Data.Repositories;

public class SchemeRepository : Repository<SchemeEntity>, ISchemeRepository
{
    public SchemeRepository()
        : base()
    {
        var grades = new List<SchemeEntity>()
        {
            new SchemeEntity()
            {
                Id = 1,
                Code = "A1",
                Name = "Project A1",
                Description = "Project A1",
            },
            new SchemeEntity()
            {
                Id = 2,
                Code = "B2",
                Name = "Project B2",
                Description = "Project B2",
            },
            new SchemeEntity()
            {
                Id = 3,
                Code = "C3",
                Name = "Project C3",
                Description = "Project C3",
            },
        };
        this.Initialise(grades);
    }

    public async Task<SchemeEntity?> GetByCodeAsync(string code)
    {
        var all = await this.GetAllAsync();

        return all.FirstOrDefault(x => x.Code.ToLower().Trim() == code.ToLower().Trim());
    }
}
