using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NSubstitute;

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
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 1,
                        Code = "S1",
                    }
                }
            },
            new ApproverEntity()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,",
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 2,
                        Code = "S2",
                    }
                }
            },
            new ApproverEntity()
            {
                Id = 3,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,",
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 3,
                        Code = "S3",
                    }
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
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 1,
                        Code = "S1",
                    }
                }
            },
            new ApproverEntity()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,",
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 2,
                        Code = "S2",
                    }
                }
            },
            new ApproverEntity()
            {
                Id = 3,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,",
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 3,
                        Code = "S3",
                    }
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
    public async Task GetApproverByEmailAddressAndSchemeAsync_ShouldReturnCorrectApprover()
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
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 1,
                        Code = "S1",
                    }
                }
            },
            new ApproverEntity()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,",
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 2,
                        Code = "S2",
                    }
                }
            },
            new ApproverEntity()
            {
                Id = 3,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,",
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 3,
                        Code = "S3",
                    }
                }
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _approverRepository.GetApproverByEmailAddressAndSchemeAsync("ApproverOne@defra.gov.uk", "S1");

        // Assert
        var approverEntity = result;
        Assert.NotNull(approverEntity);
        Assert.Equal("ApproverOne@defra.gov.uk", approverEntity.EmailAddress);
        Assert.Equal(1, approverEntity?.Schemes.Count);
        Assert.Equal("S1", approverEntity?.Schemes[0].Code);
    }

    [Fact]
    public async Task GetApproverByEmailAddressAndSchemeAsync_ShouldReturnNoApprover()
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
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 1,
                        Code = "S1",
                    }
                }
            },
            new ApproverEntity()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,",
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 2,
                        Code = "S2",
                    }
                }
            },
            new ApproverEntity()
            {
                Id = 3,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,",
                Schemes = new List<SchemeEntity>()
                {
                    new SchemeEntity()
                    {
                        Id = 3,
                        Code = "S3",
                    }
                }
            });

        await _context.SaveChangesAsync();

        // Act
        var result = await _approverRepository.GetApproverByEmailAddressAndSchemeAsync("NoApproverOne@defra.gov.uk", "S2");

        // Assert
        var approverEntity = result;
        Assert.Null(approverEntity);
    }
}

