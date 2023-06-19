using EST.MIT.Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;

public class SchemeRepositoryTests
{
    private readonly SchemeRepository _schemeRepository;
    private readonly ApprovalsContext _context;

    public SchemeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApprovalsContext>()
            .UseInMemoryDatabase(databaseName: "SchemeRepositoryTests")
            .Options;

        // Insert seed data into the database using one instance of the context
        _context = new ApprovalsContext(options);
        _schemeRepository = new SchemeRepository(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSchemes()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.Schemes.AddRange(
            new SchemeEntity()
            {
                Id = 1,
                Code = "S1",
                Name = "Scheme 1",
                Description = "This is the description for Scheme 1",
            },
            new SchemeEntity()
            {
                Id = 2,
                Code = "S2",
                Name = "Scheme 2",
                Description = "This is the description for Scheme 2",
            },
            new SchemeEntity()
            {
                Id = 3,
                Code = "S3",
                Name = "Scheme 3",
                Description = "This is the description for Scheme 3",
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _schemeRepository.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectScheme()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.Schemes.AddRange(
            new SchemeEntity()
            {
                Id = 1,
                Code = "S1",
                Name = "Scheme 1",
                Description = "This is the description for Scheme 1",
            },
            new SchemeEntity()
            {
                Id = 2,
                Code = "S2",
                Name = "Scheme 2",
                Description = "This is the description for Scheme 2",
            },
            new SchemeEntity()
            {
                Id = 3,
                Code = "S3",
                Name = "Scheme 3",
                Description = "This is the description for Scheme 3",
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _schemeRepository.GetAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("S2", result.Code);
    }

    [Fact]
    public async Task GetByCodeAsync_ShouldReturnCorrectScheme()
    {
        //Arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        _context.Schemes.AddRange(
            new SchemeEntity()
            {
                Id = 1,
                Code = "S1",
                Name = "Scheme 1",
                Description = "This is the description for Scheme 1",
            },
            new SchemeEntity()
            {
                Id = 2,
                Code = "S2",
                Name = "Scheme 2",
                Description = "This is the description for Scheme 2",
            },
            new SchemeEntity()
            {
                Id = 3,
                Code = "S3",
                Name = "Scheme 3",
                Description = "This is the description for Scheme 3",
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _schemeRepository.GetByCodeAsync("S1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("S1", result.Code);
    }
}
