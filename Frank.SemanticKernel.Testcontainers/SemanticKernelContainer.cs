namespace Frank.SemanticKernel.Testcontainers;

/// <inheritdoc cref="DockerContainer" />
[PublicAPI]
public sealed class SemanticKernelContainer : DockerContainer
{
/// <summary>
/// Initializes a new instance of the <see cref="SemanticKernelContainer" /> class.
/// </summary>
/// <param name="configuration">The container configuration.</param>
/// <param name="logger">The logger.</param>
public SemanticKernelContainer(SemanticKernelContainerConfiguration configuration, ILogger logger)
: base(configuration, logger)
{
}
}