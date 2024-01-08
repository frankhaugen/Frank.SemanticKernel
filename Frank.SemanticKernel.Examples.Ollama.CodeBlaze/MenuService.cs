using Frank.SemanticKernel.Examples.Core;

namespace Frank.SemanticKernel.Examples.Ollama.CodeBlaze;

public class MenuService : IMenuService
{
    private readonly IChatDemo _chatDemo;
    private readonly ITextGenerationDemo _textGenerationDemo;
    private readonly IConsoleHelper _consoleHelper;
    
    public MenuService(IChatDemo chatDemo, ITextGenerationDemo textGenerationDemo, IConsoleHelper consoleHelper)
    {
        _chatDemo = chatDemo;
        _textGenerationDemo = textGenerationDemo;
        _consoleHelper = consoleHelper;
        
        _consoleHelper.WriteBanner("Welcome to Ollama");
    }
    
    public async Task StartAsync()
    {
        var promptActions = new Dictionary<string, Func<string, Task>>
        {
            {"1.\tPrompt kernel", async _ => await _textGenerationDemo.StartAsync()},
            {"2.\tChat kernel", async _ => await _chatDemo.StartAsync()},
        };

        await _consoleHelper.StartAsync("Ollama", promptActions, CancellationToken.None);
        
        await StartAsync();
    }
}