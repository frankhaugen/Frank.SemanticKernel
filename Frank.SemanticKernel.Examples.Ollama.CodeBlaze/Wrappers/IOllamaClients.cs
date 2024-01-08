namespace Frank.SemanticKernel.Examples.Ollama.CodeBlaze.Wrappers;

public interface IOllamaClients
{
    OllamaChatCompletionService ChatService { get; }
    OllamaTextGenerationService GenerationService { get; }
}