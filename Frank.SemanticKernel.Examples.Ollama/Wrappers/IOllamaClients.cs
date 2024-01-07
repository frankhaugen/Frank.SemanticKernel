namespace Frank.SemanticKernel.Examples.Ollama.Wrappers;

public interface IOllamaClients
{
    IChatCompletionService CompletionService { get; }
    ITextGenerationService TextGenerationService { get; }
}