using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Data.Repositories;

public class GradeRepository : Repository<GradeEntity>, IGradeRepository
{

    public GradeRepository()
        : base()
    {
        var entities = new List<GradeEntity>()
        {
            new GradeEntity()
            {
                Id = 1,
                Code = "G1",
                Name = "Grade 1",
                Description = "This is the description for Grade 1",
            },
            new GradeEntity()
            {
                Id = 2,
                Code = "G2",
                Name = "Grade 2",
                Description = "This is the description for Grade 2",
            },
            new GradeEntity()
            {
                Id = 3,
                Code = "G3",
                Name = "Grade 3",
                Description = "This is the description for Grade 3",
            },
        };
        this.Initialise(entities);
    }
}
