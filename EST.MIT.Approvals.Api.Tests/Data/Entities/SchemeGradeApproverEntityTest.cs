using Approvals.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EST.MIT.Approvals.Api.Tests.Data.Entities;

public class SchemeGradeApproverEntityTests
{
    [Fact]
    public void SchemeGradeApproverEntity_SetProperties_ShouldSetCorrectly()
    {
        var entity = new SchemeGradeApproverEntity
        {
            SchemeId = 1,
            GradeId = 2,
            ApproverId = 3
        };

        Assert.Equal(1, entity.SchemeId);
        Assert.Equal(2, entity.GradeId);
        Assert.Equal(3, entity.ApproverId);
    }

}