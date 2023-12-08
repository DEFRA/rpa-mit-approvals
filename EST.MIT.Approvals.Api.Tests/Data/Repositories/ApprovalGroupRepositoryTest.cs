using EST.MIT.Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;

public class ApprovalGroupRepositoryTests
{
    private readonly ApprovalGroupRepository _approvalGroupRepository;
    private readonly ApprovalsContext _context;

    public ApprovalGroupRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApprovalsContext>()
            .UseInMemoryDatabase(databaseName: "ApprovalRepositoryTests")
            .Options;

        // Insert seed data into the database using one instance of the context
        _context = new ApprovalsContext(options);
        _approvalGroupRepository = new ApprovalGroupRepository(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllApprovalGroups()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.ApprovalGroups.AddRange(
            new ApprovalGroupEntity()
            {
                Id = 1,
                Code = "AG1",
            },
            new ApprovalGroupEntity()
            {
                Id = 2,
                Code = "AG2",
            },
            new ApprovalGroupEntity()
            {
                Id = 3,
                Code = "AG3",
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _approvalGroupRepository.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectApprovalGroup()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.ApprovalGroups.AddRange(
            new ApprovalGroupEntity()
            {
                Id = 1,
                Code = "AG1",
            },
            new ApprovalGroupEntity()
            {
                Id = 2,
                Code = "AG2",
            },
            new ApprovalGroupEntity()
            {
                Id = 3,
                Code = "AG3",
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _approvalGroupRepository.GetAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("AG2", result.Code);
    }

    [Fact]
    public async Task GetByCodeAsync_ShouldReturnCorrectApprovalGroup()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.ApprovalGroups.AddRange(
            new ApprovalGroupEntity()
            {
                Id = 1,
                Code = "AG1",
            },
            new ApprovalGroupEntity()
            {
                Id = 2,
                Code = "AG2",
            },
            new ApprovalGroupEntity()
            {
                Id = 3,
                Code = "AG3",
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _approvalGroupRepository.GetByCodeAsync("AG1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("AG1", result.Code);
    }
}
