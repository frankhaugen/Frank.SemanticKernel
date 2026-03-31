namespace Frank.SemanticKernel.Testcontainers;

/// <inheritdoc cref="ContainerBuilder{TBuilderEntity, TContainerEntity, TConfigurationEntity}" />
[PublicAPI]
public sealed class SemanticKernelContainerBuilder : ContainerBuilder<SemanticKernelContainerBuilder, SemanticKernelContainer, SemanticKernelContainerConfiguration>
{
/// <summary>
/// Initializes a new instance of the <see cref="SemanticKernelContainer" /> class.
/// </summary>
public SemanticKernelContainerBuilder()
: this(new SemanticKernelContainerConfiguration())
{
    // 1) To change the ContainerBuilder default configuration override the DockerResourceConfiguration property and the "SemanticKernelContainer Init()" method.
    //    Append the module configuration to base.Init() e.g. base.Init().WithImage("alpine:3.17") to set the modules' default image.

    // 2) To customize the ContainerBuilder validation override the "void Validate()" method.
    //    Use Testcontainers' Guard.Argument<TType>(TType, string) or your own guard implementation to validate the module configuration.

    // 3) Add custom builder methods to extend the ContainerBuilder capabilities such as "SemanticKernelContainer WithFrank.SemanticKernel.TestcontainersConfig(object)".
    //    Merge the current module configuration with a new instance of the immutable SemanticKernelContainerConfiguration type to update the module configuration.

    // DockerResourceConfiguration = Init();
}

/// <summary>
/// Initializes a new instance of the <see cref="SemanticKernelContainer" /> class.
/// </summary>
/// <param name="resourceConfiguration">The Docker resource configuration.</param>
private SemanticKernelContainerBuilder(SemanticKernelContainerConfiguration resourceConfiguration)
: base(resourceConfiguration)
{
    DockerResourceConfiguration = resourceConfiguration;
}

/// <inheritdoc />
protected override SemanticKernelContainerConfiguration DockerResourceConfiguration { get; }

/// <summary>
/// Sets the Frank.SemanticKernel.Testcontainers config.
/// </summary>
/// <param name="config">The Frank.SemanticKernel.Testcontainers config.</param>
/// <returns>A configured instance of <see cref="SemanticKernelContainer" />.</returns>
public SemanticKernelContainer TestcontainersConfig(object config)
{
    throw new NotImplementedException();
}

/// <inheritdoc />
public override SemanticKernelContainer Build()
{
    Validate();
    return new SemanticKernelContainer(DockerResourceConfiguration, TestcontainersSettings.Logger);
}

// /// <inheritdoc />
// protected override SemanticKernelContainer Init()
// {
//     return base.Init();
// }
//
// /// <inheritdoc />
// protected override void Validate()
// {
//     base.Validate();
// }

/// <inheritdoc />
protected override SemanticKernelContainerBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
{
    return new SemanticKernelContainerBuilder();
}

/// <inheritdoc />
protected override SemanticKernelContainerBuilder Clone(IContainerConfiguration resourceConfiguration)
{
    return Merge(DockerResourceConfiguration, new SemanticKernelContainerConfiguration(resourceConfiguration));
}

/// <inheritdoc />
protected override SemanticKernelContainerBuilder Merge(SemanticKernelContainerConfiguration oldValue, SemanticKernelContainerConfiguration newValue)
{
    return new SemanticKernelContainerBuilder();
}
}