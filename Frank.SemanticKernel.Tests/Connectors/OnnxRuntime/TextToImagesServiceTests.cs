using System.Text;
using Frank.SemanticKernel.Connectors.OnnxRuntime.StableDiffusion;
using Frank.Testing.Logging;
using Xunit.Abstractions;

namespace Frank.SemanticKernel.Tests.Connectors.OnnxRuntime;

public class TextToImagesServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TextToImagesServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    // [Fact]
    public async Task TestAsync()
    {
        // Arrange
        var service = new StableDiffusionTextToImageService(_testOutputHelper.CreateTestLogger<StableDiffusionTextToImageService>());
        var value = new DirectoryInfo(Directory.GetCurrentDirectory());
        service.AddPersistentOutputDirectory(value);
        
        // Act
        var result = await service.GenerateImageAsync("Hello world!", 512, 512);
        await File.WriteAllBytesAsync(Path.Combine(value.FullName, "test.png"), Encoding.UTF8.GetBytes(result));
        
        // Assert
        _testOutputHelper.WriteLine(Path.Combine(value.FullName, "test.png"));
    }
}