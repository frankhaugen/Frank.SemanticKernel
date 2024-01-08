using Frank.SemanticKernel.Examples.Ollama;

namespace Frank.SemanticKernel.Examples.Core;

public interface IConsoleHelper
{
    void WriteBanner(string banner);
    Task StartAsync(string appBanner, Dictionary<string, Func<string, Task>> promptActions, CancellationToken cancellationToken);
    void WriteError(string error);
    Task<T> WriteProcessingAsync<T>(string userPrompt, Func<UserInput, Task<T>> action);
    void WriteBox(string text, string header, Color borderColor);
}