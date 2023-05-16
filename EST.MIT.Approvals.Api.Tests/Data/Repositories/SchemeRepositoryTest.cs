using Approvals.Api.Data.Repositories;

namespace EST.MIT.Approvals.Api.Tests.Data.Repositories;

public class SchemeRepositoryTests
{
    private readonly SchemeRepository _schemeRepository;

    public SchemeRepositoryTests()
    {
        _schemeRepository = new SchemeRepository();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSchemes()
    {
        // Act
        var result = await _schemeRepository.GetAllAsync();

        // Assert
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task GetAsync_ShouldReturnCorrectScheme()
    {
        // Act
        var result = await _schemeRepository.GetAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("B2", result.Code);
    }

    [Fact]
    public async Task GetByCodeAsync_ShouldReturnCorrectScheme()
    {
        // Act
        var result = await _schemeRepository.GetByCodeAsync("a1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("A1", result.Code);
    }

    [Fact]
    public async Task HardDeleteAsync_ShouldRemoveScheme()
    {
        // Act
        await _schemeRepository.HardDeleteAsync(2);

        // Assert
        Assert.Null(await _schemeRepository.GetAsync(2));
        Assert.Equal(2, (await _schemeRepository.GetAllAsync()).Count());
    }

    [Fact]
    public async Task SoftDeleteAsync_ShouldMarkSchemeAsDeleted()
    {
        // Act
        await _schemeRepository.SoftDeleteAsync(2);

        // Assert
        var result = await _schemeRepository.GetAsync(2);
        Assert.NotNull(result);
        Assert.True(result.IsDeleted);
    }
}
