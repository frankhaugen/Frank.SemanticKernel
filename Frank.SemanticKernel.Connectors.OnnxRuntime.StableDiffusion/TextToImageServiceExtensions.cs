using Microsoft.SemanticKernel.TextToImage;

namespace Frank.SemanticKernel.Connectors.OnnxRuntime.StableDiffusion;

#pragma warning disable SKEXP0001, SKEXP0002, SKEXP0011, SKEXP0012
public static class TextToImageServiceExtensions
{
    /// <summary>
    /// Adds a persistent output directory to the <see cref="ITextToImageService"/> instance. When this directory is set, the service will save the generated images to this directory in addition to returning the image content from a temporary directory.
    /// </summary>
    /// <remarks>
    /// The key of the persistent output directory is <see cref="StableDiffusionTextToImageService.PERSISTENT_OUTPUT_DIRECTORY_KEY"/>.
    /// </remarks>
    /// <param name="service">The <see cref="ITextToImageService"/> instance.</param>
    /// <param name="directory">The directory to be added as the persistent output directory.</param>
    /// <typeparam name="T">The type of the <see cref="ITextToImageService"/> instance.</typeparam>
    /// <returns>The key of the persistent output directory.</returns>
    /// <exception cref="NotSupportedException">The specified text-to-image service is not supported if it is not an instance of <see cref="StableDiffusionTextToImageService"/>.</exception>
    public static string AddPersistentOutputDirectory<T>(this T service, DirectoryInfo directory) where T : StableDiffusionTextToImageService, ITextToImageService
    {
        if (service is not StableDiffusionTextToImageService stableDiffusionTextToImageService)
            throw new NotSupportedException("The specified text-to-image service is not supported.");
        const string key = StableDiffusionTextToImageService.PERSISTENT_OUTPUT_DIRECTORY_KEY;
        stableDiffusionTextToImageService.InternalAttributes[key] = directory;
        return key;
    }
}