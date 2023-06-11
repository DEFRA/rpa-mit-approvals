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
            CreatedByUserId = 100,
            ModifiedOn = now,
            ModifiedByUserId = 200,
            IsDeleted = false
        };

        Assert.Equal(1, entity.Id);
        Assert.Equal(now, entity.CreatedOn);
        Assert.Equal(100, entity.CreatedByUserId);
        Assert.Equal(now, entity.ModifiedOn);
        Assert.Equal(200, entity.ModifiedByUserId);
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