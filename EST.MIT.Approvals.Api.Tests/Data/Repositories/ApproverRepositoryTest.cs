using EST.MIT.Approvals.Api.Data.Repositories;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;


public class ApproverRepositoryTests
{
    private readonly ApproverRepository _approverRepository;

    public ApproverRepositoryTests()
    {
        _approverRepository = new ApproverRepository();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllApprovers()
    {
        // Act
        var result = await _approverRepository.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectApprover()
    {
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
        var ids = new List<int> { 1, 3 };

        // Act
        var result = await _approverRepository.GetApproversBySchemeAndGradeAsync(ids);

        // Assert
        var approverEntities = result.ToList();
        Assert.Equal(2, approverEntities.Count);
        Assert.Contains(approverEntities, a => a.Id == 1);
        Assert.Contains(approverEntities, a => a.Id == 3);
    }

    [Fact]
    public async Task HardDeleteAsync_ShouldRemoveApprover()
    {
        // Act
        await _approverRepository.HardDeleteAsync(2);

        // Assert
        Assert.Null(await _approverRepository.GetAsync(2));
        Assert.Equal(2, (await _approverRepository.GetAllAsync()).Count());
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldMarkApproverAsDeleted()
    {
        // Act
        await _approverRepository.SoftDeleteAsync(2);

        // Assert
        var result = await _approverRepository.GetAsync(2);
        Assert.NotNull(result);
        Assert.True(result.IsDeleted);
    }
}

