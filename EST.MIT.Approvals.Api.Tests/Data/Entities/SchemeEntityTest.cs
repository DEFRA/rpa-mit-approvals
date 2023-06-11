using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Tests.Data.Entities;

public class SchemeEntityTests
{
    [Fact]
    public void SchemeEntity_SetProperties_ShouldSetCorrectly()
    {
        var entity = new SchemeEntity
        {
            Name = "Scheme1",
            Description = "Scheme 1 Description",
            Code = "SCH1"
        };

        Assert.Equal("Scheme1", entity.Name);
        Assert.Equal("Scheme 1 Description", entity.Description);
        Assert.Equal("SCH1", entity.Code);
    }

}