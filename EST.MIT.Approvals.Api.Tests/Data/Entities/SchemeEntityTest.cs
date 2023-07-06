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
            Code = "SCH1"
        };

        Assert.Equal("SCH1", entity.Code);
    }

}