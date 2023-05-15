using Approvals.Api.Data.Repositories;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;

public class GradeRepositoryTests
{
    private readonly GradeRepository _gradeRepository;

    public GradeRepositoryTests()
    {
        _gradeRepository = new GradeRepository();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllGrades()
    {
        // Act
        var result = await _gradeRepository.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectGrade()
    {
        // Act
        var result = await _gradeRepository.GetAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal(5000, result.ApprovalLimit);
    }

    [Fact]
    public async Task GetByApprovalLimit_ShouldReturnCorrectGrade()
    {
        // Act
        var result = await _gradeRepository.GetByApprovalLimit(6000);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Id);
        Assert.Equal(10000, result.ApprovalLimit);
    }

    [Fact]
    public async Task HardDeleteAsync_ShouldRemoveGrade()
    {
        // Act
        await _gradeRepository.HardDeleteAsync(2);

        // Assert
        Assert.Null(await _gradeRepository.GetAsync(2));
        Assert.Equal(2, (await _gradeRepository.GetAllAsync()).Count());
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldMarkGradeAsDeleted()
    {
        // Act
        await _gradeRepository.SoftDeleteAsync(2);

        // Assert
        var result = await _gradeRepository.GetAsync(2);
        Assert.NotNull(result);
        Assert.True(result.IsDeleted);
    }
}
