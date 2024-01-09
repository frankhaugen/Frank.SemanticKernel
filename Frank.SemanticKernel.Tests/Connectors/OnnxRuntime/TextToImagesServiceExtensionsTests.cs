using FluentAssertions;
using Frank.SemanticKernel.Connectors.OnnxRuntime.StableDiffusion;
using Frank.Testing.Logging;
using Xunit.Abstractions;

namespace Frank.SemanticKernel.Tests.Connectors.OnnxRuntime;

public class TextToImagesServiceExtensionsTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TextToImagesServiceExtensionsTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void Test1()
    {
        // Arrange
        var service = new StableDiffusionTextToImageService(_testOutputHelper.CreateTestLogger<StableDiffusionTextToImageService>());
        var value = new DirectoryInfo(Directory.GetCurrentDirectory());
        service.AddPersistentOutputDirectory(value);
        
        // Act
        var result = service.Attributes[StableDiffusionTextToImageService.PERSISTENT_OUTPUT_DIRECTORY_KEY] as DirectoryInfo;
        
        // Assert
        Assert.NotNull(result);
        _testOutputHelper.WriteLine(result.FullName);
        result.Should().Be(value);
    }
}