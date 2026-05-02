namespace Frank.SemanticKernel.Testcontainers;

/// <inheritdoc cref="ContainerConfiguration" />
[PublicAPI]
public sealed class SemanticKernelContainerConfiguration : ContainerConfiguration
{
/// <summary>
/// Initializes a new instance of the <see cref="SemanticKernelContainerConfiguration" /> class.
/// </summary>
/// <param name="config">The Frank.SemanticKernel.Testcontainers config.</param>
public SemanticKernelContainerConfiguration(object config = null)
{
    // // Sets the custom builder methods property values.
    // Config = config;
}

/// <summary>
/// Initializes a new instance of the <see cref="SemanticKernelContainerConfiguration" /> class.
/// </summary>
/// <param name="resourceConfiguration">The Docker resource configuration.</param>
public SemanticKernelContainerConfiguration(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
: base(resourceConfiguration)
{
    // Passes the configuration upwards to the base implementations to create an updated immutable copy.
}

/// <summary>
/// Initializes a new instance of the <see cref="SemanticKernelContainerConfiguration" /> class.
/// </summary>
/// <param name="resourceConfiguration">The Docker resource configuration.</param>
public SemanticKernelContainerConfiguration(IContainerConfiguration resourceConfiguration)
: base(resourceConfiguration)
{
    // Passes the configuration upwards to the base implementations to create an updated immutable copy.
}

/// <summary>
/// Initializes a new instance of the <see cref="SemanticKernelContainerConfiguration" /> class.
/// </summary>
/// <param name="resourceConfiguration">The Docker resource configuration.</param>
public SemanticKernelContainerConfiguration(SemanticKernelContainerConfiguration resourceConfiguration)
: this(new SemanticKernelContainerConfiguration(), resourceConfiguration)
{
    // Passes the configuration upwards to the base implementations to create an updated immutable copy.
}

/// <summary>
/// Initializes a new instance of the <see cref="SemanticKernelContainerConfiguration" /> class.
/// </summary>
/// <param name="oldValue">The old Docker resource configuration.</param>
/// <param name="newValue">The new Docker resource configuration.</param>
public SemanticKernelContainerConfiguration(SemanticKernelContainerConfiguration oldValue, SemanticKernelContainerConfiguration newValue)
: base(oldValue, newValue)
{
    // // Create an updated immutable copy of the module configuration.
    // Config = BuildConfiguration.Combine(oldValue.Config, newValue.Config);
}

// /// <summary>
// /// Gets the Frank.SemanticKernel.Testcontainers config.
// /// </summary>
// public object Config { get; }
}