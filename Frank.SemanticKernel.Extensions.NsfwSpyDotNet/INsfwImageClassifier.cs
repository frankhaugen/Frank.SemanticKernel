using NsfwSpyNS;

namespace Frank.SemanticKernel.Extensions.NsfwSpyDotNet;

internal interface INsfwImageClassifier
{
    /// <summary>
    /// Determines if an image is Not Safe for Work (NSFW) based on the provided image bytes.
    /// </summary>
    /// <param name="imageBytes">The image bytes to evaluate</param>
    /// <returns>
    /// True if the image is NSFW; otherwise, false.
    /// </returns>
    bool IsNsfw(ReadOnlyMemory<byte> imageBytes);

    /// <summary>
    /// Classifies the given image and returns the result.
    /// </summary>
    /// <param name="imageBytes">The byte array containing the image data.</param>
    /// <returns>The NsfwSpyResult representing the classification result of the image.</returns>
    NsfwSpyResult Classify(ReadOnlyMemory<byte> imageBytes);
}