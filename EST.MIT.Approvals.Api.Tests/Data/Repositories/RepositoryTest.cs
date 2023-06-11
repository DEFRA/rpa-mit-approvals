using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using EST.MIT.Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;


public class RepositoryTests
{
    private class TestEntity : BaseEntity
    {
        public string Name { get; set; }
    }

    private class TestRepository : Repository<TestEntity>
    {
        public TestRepository()
        {
            Initialise(new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Test 1" },
                new TestEntity { Id = 2, Name = "Test 2" },
                new TestEntity { Id = 3, Name = "Test 3" }
            });
        }
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var repository = new TestRepository();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectEntity()
    {
        // Arrange
        var repository = new TestRepository();

        // Act
        var result = await repository.GetAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("Test 2", result.Name);
    }

    [Fact]
    public async Task HardDeleteAsync_ShouldRemoveEntity()
    {
        // Arrange
        var repository = new TestRepository();

        // Act
        await repository.HardDeleteAsync(2);

        // Assert
        Assert.Null(await repository.GetAsync(2));
        Assert.Equal(2, (await repository.GetAllAsync()).Count());
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldMarkEntityAsDeleted()
    {
        // Arrange
        var repository = new TestRepository();

        // Act
        await repository.SoftDeleteAsync(2);

        // Assert
        var result = await repository.GetAsync(2);
        Assert.NotNull(result);
        Assert.True(result.IsDeleted);
    }
}
