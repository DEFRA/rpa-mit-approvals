using Approvals.Api.Data.Repositories;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;

public class SchemeGradeApproverRepositoryTests
{
    private readonly SchemeGradeApproverRepository _schemeGradeApproverRepository;

    public SchemeGradeApproverRepositoryTests()
    {
        _schemeGradeApproverRepository = new SchemeGradeApproverRepository();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSchemeGradeApprovers()
    {
        // Act
        var result = await _schemeGradeApproverRepository.GetAllAsync();

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectSchemeGradeApprover()
    {
        // Act
        var result = await _schemeGradeApproverRepository.GetAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(1, result.SchemeId);
        Assert.Equal(1, result.GradeId);
        Assert.Equal(1, result.ApproverId);
    }

    [Fact]
    public async Task GetAllBySchemeAndGrade_ShouldReturnCorrectSchemeGradeApprovers()
    {
        // Act
        var result = await _schemeGradeApproverRepository.GetAllBySchemeAndGrade(1, 1);

        // Assert
        Assert.Single(result);
        var entity = result.First();
        Assert.Equal(1, entity.SchemeId);
        Assert.Equal(1, entity.GradeId);
    }

    [Fact]
    public async Task HardDeleteAsync_ShouldRemoveSchemeGradeApprover()
    {
        // Act
        await _schemeGradeApproverRepository.HardDeleteAsync(1);

        // Assert
        Assert.Null(await _schemeGradeApproverRepository.GetAsync(1));
        Assert.Empty(await _schemeGradeApproverRepository.GetAllAsync());
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldMarkSchemeGradeApproverAsDeleted()
    {
        // Act
        await _schemeGradeApproverRepository.SoftDeleteAsync(1);

        // Assert
        var result = await _schemeGradeApproverRepository.GetAsync(1);
        Assert.NotNull(result);
        Assert.True(result.IsDeleted);
    }
}
