using Approvals.Api.Data.Entities;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

namespace Approvals.Api.Data.Repositories;

public class GradeRepository : Repository<GradeEntity>, IGradeRepository
{

    public GradeRepository()
        : base()
    {
        var grades = new List<GradeEntity>()
        {
            new GradeEntity()
            {
                Id = 1,
                ApprovalLimit = 1000,
                Name = "Low",
                Description = "Low",
            },
            new GradeEntity()
            {
                Id = 2,
                ApprovalLimit = 5000,
                Name = "Medium",
                Description = "Medium",
            },
            new GradeEntity()
            {
                Id = 3,
                ApprovalLimit = 10000,
                Name = "High",
                Description = "High",
            },
        };
        this.Initialise(grades);
    }

    public async Task<GradeEntity?> GetByApprovalLimit(decimal approvalLimit)
    {
        var all = await this.GetAllAsync();

        return all.FirstOrDefault(x => x.ApprovalLimit >= approvalLimit);
    }
}
