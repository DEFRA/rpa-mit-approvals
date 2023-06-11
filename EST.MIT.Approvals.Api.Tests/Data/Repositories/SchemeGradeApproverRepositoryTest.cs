using EST.MIT.Approvals.Api.Data.Repositories;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;

public class SchemeGradeApproverRepositoryTests
{
    private readonly SchemeGradeApprovalRepository _schemeGradeApprovalRepository;

    public SchemeGradeApproverRepositoryTests()
    {
        _schemeGradeApprovalRepository = new SchemeGradeApprovalRepository();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSchemeGradeApprovers()
    {
        // Act
        var result = await _schemeGradeApprovalRepository.GetAllAsync();

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectSchemeGradeApprover()
    {
        // Act
        var result = await _schemeGradeApprovalRepository.GetAsync(1);

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
        // Act
        var result = await _schemeGradeApprovalRepository.GetAllBySchemeGradesBySchemeAndApprovalLimit(1, 1);

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

    [Fact]
    public async Task HardDeleteAsync_ShouldRemoveSchemeGradeApprover()
    {
        // Act
        await _schemeGradeApprovalRepository.HardDeleteAsync(1);

        // Assert
        Assert.Null(await _schemeGradeApprovalRepository.GetAsync(1));
        Assert.Empty(await _schemeGradeApprovalRepository.GetAllAsync());
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldMarkSchemeGradeApproverAsDeleted()
    {
        // Act
        await _schemeGradeApprovalRepository.SoftDeleteAsync(1);

        // Assert
        var result = await _schemeGradeApprovalRepository.GetAsync(1);
        Assert.NotNull(result);
        Assert.True(result.IsDeleted);
    }
}
