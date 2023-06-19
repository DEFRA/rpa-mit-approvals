using EST.MIT.Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;

public class SchemeGradeApproverRepositoryTests
{
    private readonly SchemeApprovalGradeRepository _schemeApprovalGradeRepository;
    private readonly ApprovalsContext _context;

    public SchemeGradeApproverRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApprovalsContext>()
            .UseInMemoryDatabase(databaseName: "SchemeGradeApproverRepositoryTests")
            .Options;

        // Insert seed data into the database using one instance of the context
        _context = new ApprovalsContext(options);
        _schemeApprovalGradeRepository = new SchemeApprovalGradeRepository(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSchemeGradeApprovers()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.SchemeApprovalGrades.AddRange(
            new SchemeApprovalGradeEntity()
            {
                Id = 1,
                SchemeGrade = new SchemeGradeEntity()
                {
                    Id = 1,
                    Scheme = new SchemeEntity()
                    {
                        Id = 1,
                        Code = "S1",
                        Name = "Scheme 1",
                        Description = "This is the description for Scheme 1",
                    },
                    Grade = new GradeEntity()
                    {
                        Id = 1,
                        Code = "G1",
                        Name = "Grade 1",
                        Description = "This is the description for Grade 1",
                    },
                },
                ApprovalLimit = 1000M,
                IsUnlimited = false,
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _schemeApprovalGradeRepository.GetAllAsync();

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectSchemeGradeApprover()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.SchemeApprovalGrades.AddRange(
            new SchemeApprovalGradeEntity()
            {
                Id = 1,
                SchemeGrade = new SchemeGradeEntity()
                {
                    Id = 1,
                    Scheme = new SchemeEntity()
                    {
                        Id = 1,
                        Code = "S1",
                        Name = "Scheme 1",
                        Description = "This is the description for Scheme 1",
                    },
                    Grade = new GradeEntity()
                    {
                        Id = 1,
                        Code = "G1",
                        Name = "Grade 1",
                        Description = "This is the description for Grade 1",
                    },
                },
                ApprovalLimit = 1000M,
                IsUnlimited = false,
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _schemeApprovalGradeRepository.GetAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(1, result.SchemeGrade?.Scheme?.Id);
        Assert.Equal(1, result.SchemeGrade?.Grade?.Id);
        Assert.False(result.IsUnlimited);
        Assert.Equal(1000M, result.ApprovalLimit);
    }

    [Fact]
    public async Task GetAllBySchemeGradesBySchemeAndApprovalLimit_ShouldReturnCorrectSchemeGradeApprovers()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.SchemeApprovalGrades.AddRange(
            new SchemeApprovalGradeEntity()
            {
                Id = 1,
                SchemeGrade = new SchemeGradeEntity()
                {
                    Id = 1,
                    Scheme = new SchemeEntity()
                    {
                        Id = 1,
                        Code = "S1",
                        Name = "Scheme 1",
                        Description = "This is the description for Scheme 1",
                    },
                    Grade = new GradeEntity()
                    {
                        Id = 1,
                        Code = "G1",
                        Name = "Grade 1",
                        Description = "This is the description for Grade 1",
                    },
                },
                ApprovalLimit = 1000M,
                IsUnlimited = false,
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _schemeApprovalGradeRepository.GetAllBySchemeAndApprovalLimit(1, 1);

        // Assert
        var schemeGradeApprovalEntities = result.ToList();
        Assert.Single(schemeGradeApprovalEntities);
        var entity = schemeGradeApprovalEntities.First();
        Assert.Equal(1, entity.Id);
        Assert.Equal(1, entity.SchemeGrade?.Scheme?.Id);
        Assert.Equal(1, entity.SchemeGrade?.Grade?.Id);
        Assert.False(entity.IsUnlimited);
        Assert.Equal(1000M, entity.ApprovalLimit);
    }
}
