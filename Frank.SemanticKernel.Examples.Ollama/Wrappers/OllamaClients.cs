namespace Frank.SemanticKernel.Examples.Ollama.Wrappers;

public class OllamaClients : IOllamaClients
{
    private readonly OllamaChatCompletionService _lazyCompletionService;
    private readonly OllamaTextGenerationService _lazyTextGenerationService;

    public OllamaClients(IHttpClientFactory httpClientFactory, IOptions<Configurations.Ollama> optionsUncensoredLlama2, ILoggerFactory loggerFactory)
    {
        _lazyCompletionService = new OllamaChatCompletionService(optionsUncensoredLlama2.Value.ModelName, optionsUncensoredLlama2.Value.BaseUrl, httpClientFactory.CreateClient(), loggerFactory);
        _lazyTextGenerationService = new OllamaTextGenerationService(optionsUncensoredLlama2.Value.ModelName, optionsUncensoredLlama2.Value.BaseUrl, httpClientFactory.CreateClient(), loggerFactory);
    }
    
    public IChatCompletionService CompletionService => _lazyCompletionService;
    
    public ITextGenerationService TextGenerationService => _lazyTextGenerationService;
}