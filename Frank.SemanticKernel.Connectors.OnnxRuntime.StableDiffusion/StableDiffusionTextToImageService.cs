using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;
using SixLabors.ImageSharp.Formats.Png;
using StableDiffusion.ML.OnnxRuntime;

namespace Frank.SemanticKernel.Connectors.OnnxRuntime.StableDiffusion;

#pragma warning disable SKEXP0001, SKEXP0002, SKEXP0011, SKEXP0012
/// <summary>
/// Service for converting text descriptions to images using the Stable Diffusion model.
/// </summary>
public class StableDiffusionTextToImageService : ITextToImageService
{
    public const string SAFETY_CHECKER_PATH = "safety_checker/model.onnx";
    public const string VAE_DECODER_PATH = "vae_decoder/model.onnx";
    public const string UNET_PATH = "unet/model.onnx";
    public const string TEXT_ENCODER = "text_encoder/model.onnx";
    public const string PERSISTENT_OUTPUT_DIRECTORY_KEY = "PersistentOutputDirectory";
    
    private readonly ILogger<StableDiffusionTextToImageService> _logger;

    public StableDiffusionTextToImageService(ILogger<StableDiffusionTextToImageService> logger)
    {
        _logger = logger;
    }

    internal Dictionary<string, object?> InternalAttributes { get; } = new Dictionary<string, object?>();
    
    public IReadOnlyDictionary<string, object?> Attributes => InternalAttributes;
    
    public async Task<string> GenerateImageAsync(string description, int width, int height, Kernel? kernel = null, CancellationToken cancellationToken = default)
    {
        var directory = InternalAttributes[PERSISTENT_OUTPUT_DIRECTORY_KEY] as DirectoryInfo;
        if (directory == null)
            throw new InvalidOperationException($"The '{PERSISTENT_OUTPUT_DIRECTORY_KEY}' attribute must be set before calling this method.");
        
        var modelsDirectory = directory.CreateSubdirectory("models");
        EnsureModelsExist(modelsDirectory);
        
        //test how long this takes to execute
        var watch = System.Diagnostics.Stopwatch.StartNew();

        //Default args
        var prompt = "A cat with a lightsaber fighting a dog with a two-sided lightsaber-staff.";
        _logger.LogInformation(prompt);

        var config = new StableDiffusionConfig
        {
            // Number of denoising steps
            NumInferenceSteps = 15,
            // Scale for classifier-free guidance
            GuidanceScale = 7.5,
            // Set your preferred Execution Provider. Currently (GPU, DirectML, CPU) are supported in this project.
            // ONNX Runtime supports many more than this. Learn more here: https://onnxruntime.ai/docs/execution-providers/
            // The config is defaulted to CUDA. You can override it here if needed.
            // To use DirectML EP intall the Microsoft.ML.OnnxRuntime.DirectML and uninstall Microsoft.ML.OnnxRuntime.GPU
            ExecutionProviderTarget = StableDiffusionConfig.ExecutionProvider.DirectML,
            // Set GPU Device ID.
            DeviceId = 0,
            // Update paths to your models
            TextEncoderOnnxPath = Path.Combine(modelsDirectory.FullName, TEXT_ENCODER),
            UnetOnnxPath = Path.Combine(modelsDirectory.FullName, UNET_PATH),
            VaeDecoderOnnxPath = Path.Combine(modelsDirectory.FullName, VAE_DECODER_PATH),
            SafetyModelPath = Path.Combine(modelsDirectory.FullName, SAFETY_CHECKER_PATH),
        };

        // Inference Stable Diff
        var image = UNet.Inference(prompt, config);

        // If image failed or was unsafe it will return null.
        if (image == null)
        {
            _logger.LogError("Unable to create image, please try again.");
        }
        // Stop the timer
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        _logger.LogInformation("Time taken: " + elapsedMs + "ms");
        
        using var imageStream = new MemoryStream(); 
        await image!.SaveAsync(imageStream, new PngEncoder(), cancellationToken);
        return Encoding.UTF8.GetString(imageStream.ToArray());
    }

    /// <summary>
    /// Ensures that required models exist in the specified models directory.
    /// Throws a FileNotFoundException if any of the required models are not found.
    /// </summary>
    /// <param name="modelsDirectory">The directory where the models are located. e.g. "D:\repos\StableDiffusion\StableDiffusion\models"</param>
    public static void EnsureModelsExist(FileSystemInfo modelsDirectory)
    {
        var safetyCheckerPath = Path.Combine(modelsDirectory.FullName, SAFETY_CHECKER_PATH);
        var vaeDecoderPath = Path.Combine(modelsDirectory.FullName, VAE_DECODER_PATH);
        var unetPath = Path.Combine(modelsDirectory.FullName, UNET_PATH);
        var textEncoderPath = Path.Combine(modelsDirectory.FullName, TEXT_ENCODER);

        if (!modelsDirectory.Exists)
            throw new DirectoryNotFoundException($"The models directory was not found at '{modelsDirectory.FullName}'.");

        if (!File.Exists(safetyCheckerPath))
            throw new FileNotFoundException($"The safety checker model was not found at '{safetyCheckerPath}'.");

        if (!File.Exists(vaeDecoderPath))
            throw new FileNotFoundException($"The VAE decoder model was not found at '{vaeDecoderPath}'.");

        if (!File.Exists(unetPath))
            throw new FileNotFoundException($"The UNET model was not found at '{unetPath}'.");

        if (!File.Exists(textEncoderPath))
            throw new FileNotFoundException($"The text encoder model was not found at '{textEncoderPath}'.");
    }
}