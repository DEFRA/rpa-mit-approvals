using EST.MIT.Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;


public class ApproverRepositoryTests
{
    private readonly ApproverRepository _approverRepository;
    private readonly ApprovalsContext _context;

    public ApproverRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApprovalsContext>()
            .UseInMemoryDatabase(databaseName: "ApproverRepositoryTests")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Options;

        // Insert seed data into the database using one instance of the context
        _context = new ApprovalsContext(options);

        _approverRepository = new ApproverRepository(_context);
    }
    

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllApprovers()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.Approvers.AddRange(
            new ApproverEntity()
            {
                Id = 1,
                EmailAddress = "ApproverOne@defra.gov.uk",
                FirstName = "Approver",
                LastName = "One,",
                SchemeGrades = new List<ApproverSchemeGradeEntity>()
                {
                    new ApproverSchemeGradeEntity() { Id = 1 }
                }
            },
            new ApproverEntity()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,",
                SchemeGrades = new List<ApproverSchemeGradeEntity>()
                {
                    new ApproverSchemeGradeEntity() { Id = 2 }
                }
            },
            new ApproverEntity()
            {
                Id = 3,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,",
                SchemeGrades = new List<ApproverSchemeGradeEntity>()
                {
                    new ApproverSchemeGradeEntity() { Id = 3 }
                }
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _approverRepository.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectApprover()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.Approvers.AddRange(
            new ApproverEntity()
            {
                Id = 1,
                EmailAddress = "ApproverOne@defra.gov.uk",
                FirstName = "Approver",
                LastName = "One,",
                SchemeGrades = new List<ApproverSchemeGradeEntity>()
                {
                    new ApproverSchemeGradeEntity() { Id = 1 }
                }
            },
            new ApproverEntity()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,",
                SchemeGrades = new List<ApproverSchemeGradeEntity>()
                {
                    new ApproverSchemeGradeEntity() { Id = 2 }
                }
            },
            new ApproverEntity()
            {
                Id = 3,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,",
                SchemeGrades = new List<ApproverSchemeGradeEntity>()
                {
                    new ApproverSchemeGradeEntity() { Id = 3 }
                }
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _approverRepository.GetAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("ApproverTwo@defra.gov.uk", result.EmailAddress);
    }

    [Fact]
    public async Task GetApproversByIdsAsync_ShouldReturnCorrectApprovers()
    {
        // Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.Approvers.AddRange(
            new ApproverEntity()
            {
                Id = 1,
                EmailAddress = "ApproverOne@defra.gov.uk",
                FirstName = "Approver",
                LastName = "One,",
                SchemeGrades = new List<ApproverSchemeGradeEntity>()
                {
                    new ApproverSchemeGradeEntity() { Id = 1 }
                }
            },
            new ApproverEntity()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,",
                SchemeGrades = new List<ApproverSchemeGradeEntity>()
                {
                    new ApproverSchemeGradeEntity() { Id = 2 }
                }
            },
            new ApproverEntity()
            {
                Id = 3,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,",
                SchemeGrades = new List<ApproverSchemeGradeEntity>()
                {
                    new ApproverSchemeGradeEntity() { Id = 3 }
                }
            });

        await _context.SaveChangesAsync();

        var ids = new List<int> { 1, 3 };

        // Act
        var result = await _approverRepository.GetApproversBySchemeAndGradeAsync(ids);

        // Assert
        var approverEntities = result.ToList();
        Assert.Equal(2, approverEntities.Count);
        Assert.Contains(approverEntities, a => a.Id == 1);
        Assert.Contains(approverEntities, a => a.Id == 3);
    }
}

