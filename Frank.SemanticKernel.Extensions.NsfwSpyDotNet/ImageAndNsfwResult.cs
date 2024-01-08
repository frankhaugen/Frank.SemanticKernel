using NsfwSpyNS;

namespace Frank.SemanticKernel.Extensions.NsfwSpyDotNet;

/// <summary>
/// Represents the result of an image analysis that includes the image data and NSFW detection result.
/// </summary>
public record struct ImageAndNsfwResult(ReadOnlyMemory<byte> Image, NsfwSpyResult NsfwResult);