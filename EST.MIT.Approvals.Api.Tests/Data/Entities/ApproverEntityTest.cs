using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Tests.Data.Entities;

public class ApproverEntityTests
{
    [Fact]
    public void ApproverEntity_SetProperties_ShouldSetCorrectly()
    {
        var entity = new ApproverEntity
        {
            EmailAddress = "test@example.com",
            FirstName = "John",
            LastName = "Doe"
        };

        Assert.Equal("test@example.com", entity.EmailAddress);
        Assert.Equal("John", entity.FirstName);
        Assert.Equal("Doe", entity.LastName);
    }
}

