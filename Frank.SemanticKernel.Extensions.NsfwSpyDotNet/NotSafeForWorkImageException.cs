using NsfwSpyNS;

namespace Frank.SemanticKernel.Extensions.NsfwSpyDotNet;

/// <summary>
/// Represents an exception that is thrown when an image generated from a given prompt is not safe for work.
/// It typically indicates that the image contains nudity or sexual content.
/// </summary>
public class NotSafeForWorkImageException : Exception
{
    /// <summary>
    /// Represents an exception that is thrown when an image generated from a prompt is not safe for work.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public NotSafeForWorkImageException(string prompt) : base($"The image generate from prompt '{prompt}' is not safe for work! Mean the image contains nudity or sexual content. Please try again.")
    {
    }

    /// <summary>
    /// Represents an exception that is thrown when an image generated from a prompt is not safe for work.
    /// </summary>
    /// <remarks>
    /// This exception is thrown when the generated image contains nudity or sexual content.
    /// </remarks>
    public NotSafeForWorkImageException(string prompt, NsfwSpyResult nsfwResult) : this($"The image generate from prompt '{prompt}' is not safe for work! Mean the image contains nudity or sexual content. Please try again.")
    {
        NsfwResult = nsfwResult;
    }

    /// <summary>
    /// Gets the NSFW (Not Safe For Work) result of the operation.
    /// </summary>
    public NsfwSpyResult? NsfwResult { get; }
}