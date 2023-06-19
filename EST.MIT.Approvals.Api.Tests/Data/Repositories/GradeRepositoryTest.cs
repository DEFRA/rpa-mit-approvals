using EST.MIT.Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;

public class GradeRepositoryTests
{
    private readonly GradeRepository _gradeRepository;
    private readonly ApprovalsContext _context;

    public GradeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApprovalsContext>()
            .UseInMemoryDatabase(databaseName: "GradeRepositoryTests")
            .Options;

        // Insert seed data into the database using one instance of the context
        _context = new ApprovalsContext(options);
        _gradeRepository = new GradeRepository(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllGrades()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.Grades.AddRange(
            new GradeEntity()
            {
                Id = 1,
                Code = "G1",
                Name = "Grade 1",
                Description = "This is the description for Grade 1",
            },
            new GradeEntity()
            {
                Id = 2,
                Code = "G2",
                Name = "Grade 2",
                Description = "This is the description for Grade 2",
            },
            new GradeEntity()
            {
                Id = 3,
                Code = "G3",
                Name = "Grade 3",
                Description = "This is the description for Grade 3",
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _gradeRepository.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectGrade()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.Grades.AddRange(
            new GradeEntity()
            {
                Id = 1,
                Code = "G1",
                Name = "Grade 1",
                Description = "This is the description for Grade 1",
            },
            new GradeEntity()
            {
                Id = 2,
                Code = "G2",
                Name = "Grade 2",
                Description = "This is the description for Grade 2",
            },
            new GradeEntity()
            {
                Id = 3,
                Code = "G3",
                Name = "Grade 3",
                Description = "This is the description for Grade 3",
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _gradeRepository.GetAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
    }
}
