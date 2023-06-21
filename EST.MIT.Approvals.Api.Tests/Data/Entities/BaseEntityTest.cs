using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Tests.Data.Entities;

public class BaseEntityTests
{
    public class TestEntity : BaseEntity
    {
        // This is a derived class solely for testing purposes
    }

    [Fact]
    public void BaseEntity_SetProperties_ShouldSetCorrectly()
    {
        var now = DateTime.UtcNow;
        var entity = new TestEntity
        {
            Id = 1,
            CreatedOn = now,
            ModifiedOn = now,
            IsDeleted = false
        };

        Assert.Equal(1, entity.Id);
        Assert.Equal(now, entity.CreatedOn);
        Assert.Equal(now, entity.ModifiedOn);
        Assert.False(entity.IsDeleted);
    }

    [Fact]
    public void BaseEntity_DefaultValues_ShouldSetCorrectly()
    {
        var entity = new TestEntity();

        Assert.True(entity.CreatedOn <= DateTime.UtcNow);
        Assert.False(entity.IsDeleted);
    }

}