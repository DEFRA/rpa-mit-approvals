using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Tests.Data.Entities;

public class GradeEntityTests
{
    [Fact]
    public void GradeEntity_SetProperties_ShouldSetCorrectly()
    {
        var entity = new GradeEntity
        {
            Name = "Grade1",
            Description = "Grade 1 Description",
        };

        Assert.Equal("Grade1", entity.Name);
        Assert.Equal("Grade 1 Description", entity.Description);
    }
}