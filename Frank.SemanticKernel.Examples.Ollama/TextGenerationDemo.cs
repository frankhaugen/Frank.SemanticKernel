using Frank.SemanticKernel.Examples.Core;

public class TextGenerationDemo : ITextGenerationDemo
{
    private readonly IConsoleHelper _consoleHelper;
    private readonly IOllamaClients _ollamaClients;

    public TextGenerationDemo(IConsoleHelper consoleHelper, IOllamaClients ollamaClients)
    {
        _consoleHelper = consoleHelper;
        _ollamaClients = ollamaClients;
    }

    public async Task StartAsync()
    {
        var promptActions = new Dictionary<string, Func<string, Task>>
        {
            {"1.\tPrompt kernel", async _ => await PromptUserAsync()},
        };

        await _consoleHelper.StartAsync("Ollama", promptActions, CancellationToken.None);
    }

    private async Task PromptUserAsync()
    {
        var userPrompt = AnsiConsole.Prompt(new TextPrompt<string>("What are you looking to do today ?").PromptStyle("teal"));
        
        await _consoleHelper.WriteProcessingAsync(userPrompt, async ctx =>
        {
            var result = await _ollamaClients.TextGenerationService.GetTextContentsAsync(ctx.Text ?? string.Empty);
            var text = string.Join("", result.Select(x => x.Text));
        
            _consoleHelper.WriteBox("Result", "AI", text, "Result", Color.Green);
            
            return await Task.FromResult(text);
        });
        
    }
}