using NsfwSpyNS;

namespace Frank.SemanticKernel.Extensions.NsfwSpyDotNet;

/// <summary>
/// The NsfwImageClassifier class is responsible for classifying images
/// and determining if they are NSFW (Not Safe For Work).
/// </summary>
internal class NsfwImageClassifier(INsfwSpy classifier) : INsfwImageClassifier
{
    /// <Inheritdoc/>
    public bool IsNsfw(ReadOnlyMemory<byte> imageBytes) => Classify(imageBytes).IsNsfw;

    /// <Inheritdoc/>
    public NsfwSpyResult Classify(ReadOnlyMemory<byte> imageBytes) => classifier.ClassifyImage(imageBytes.ToArray());
}