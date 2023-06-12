using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories;

public class SchemeRepository : Repository<SchemeEntity>, ISchemeRepository
{
    public SchemeRepository()
        : base()
    {
        var entities = new List<SchemeEntity>()
        {
            new SchemeEntity()
            {
                Id = 1,
                Code = "S1",
                Name = "Scheme 1",
                Description = "This is the description for Scheme 1",
            },
            new SchemeEntity()
            {
                Id = 2,
                Code = "S2",
                Name = "Scheme 2",
                Description = "This is the description for Scheme 2",
            },
            new SchemeEntity()
            {
                Id = 3,
                Code = "S3",
                Name = "Scheme 3",
                Description = "This is the description for Scheme 3",
            },
        };
        this.Initialise(entities);
    }

    public async Task<SchemeEntity?> GetByCodeAsync(string code)
    {
        var all = await this.GetAllAsync();

        return all.FirstOrDefault(x => x.Code.ToLower().Trim() == code.ToLower().Trim());
    }
}
