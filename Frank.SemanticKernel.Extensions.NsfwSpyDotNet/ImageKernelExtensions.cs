using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;
using NsfwSpyNS;

namespace Frank.SemanticKernel.Extensions.NsfwSpyDotNet;

/// <summary>
/// Provides extension methods for the ImageKernel class.
/// </summary>
#pragma warning disable SKEXP0001, SKEXP0002, SKEXP0011, SKEXP0012
public static class ImageKernelExtensions
{
    /// <summary>
    /// Generates a safe-for-work image asynchronously based on the given prompt.
    /// </summary>
    /// <param name="imageService">The instance of the text-to-image service.</param>
    /// <param name="prompt">The prompt used to generate the image.</param>
    /// <param name="width">The width of the generated image (default is 512).</param>
    /// <param name="height">The height of the generated image (default is 512).</param>
    /// <param name="kernel">The kernel to be used for image generation (optional).</param>
    /// <param name="throwOnNsfw">Indicates whether to throw an exception for NSFW (Not Safe For Work) images (default is true).</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task<ReadOnlyMemory<byte>> GenerateSafeForWorkImageAsync(this ITextToImageService imageService, string prompt, int width = 512, int height = 512, Kernel? kernel = null, bool throwOnNsfw = true)
    {
        var nsfwSpyDotNetClassifier = new NsfwImageClassifier(new NsfwSpy());
        var image = await imageService.GenerateImageAsync(prompt, width, height, kernel);
        var content = Encoding.UTF8.GetBytes(image);
        var isSafe = nsfwSpyDotNetClassifier.IsNsfw(content);
        return !isSafe && throwOnNsfw ? throw new NotSafeForWorkImageException(prompt) : !isSafe ? new ReadOnlyMemory<byte>() : content;
    }

    /// <summary>
    /// Generates a safe for work image and determines its NSFW (not safe for work) classification result asynchronously.
    /// </summary>
    /// <param name="imageService">The text to image service used to generate the image.</param>
    /// <param name="prompt">The prompt used for generating the image.</param>
    /// <param name="width">The width of the generated image (optional). Defaults to 512.</param>
    /// <param name="height">The height of the generated image (optional). Defaults to 512.</param>
    /// <param name="kernel">The kernel used for image generation (optional).</param>
    /// <returns>An instance of ImageAndNsfwResult representing the generated image content and its NSFW classification result.</returns>
    public static async Task<ImageAndNsfwResult> GenerateSafeForWorkImageAndNsfwResultAsync(this ITextToImageService imageService, string prompt, int width = 512, int height = 512, Kernel? kernel = null)
    {
        var nsfwSpyDotNetClassifier = new NsfwImageClassifier(new NsfwSpy());
        var image = await imageService.GenerateImageAsync(prompt, width, height, kernel);
        var content = Encoding.UTF8.GetBytes(image);
        var nsfwResult = nsfwSpyDotNetClassifier.Classify(content);
        return new ImageAndNsfwResult(content, nsfwResult);
    }
}