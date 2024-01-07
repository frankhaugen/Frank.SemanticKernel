using Frank.SemanticKernel.Examples.Ollama;

namespace Frank.SemanticKernel.Examples.Core;

public class ConsoleHelper : IConsoleHelper
{
    public async Task StartAsync(string appBanner, Dictionary<string, Func<string, Task>> promptActions, CancellationToken cancellationToken)
    {
        AnsiConsole.Write(new FigletText(appBanner).Color(Color.Green));
        AnsiConsole.WriteLine("");

        await WriteProcessingAsync<string>("Initializing...", async ctx => await Task.FromResult("Initialized"));

        if (!promptActions.ContainsKey("Exit") && !promptActions.ContainsKey("exit") && !promptActions.ContainsKey("EXIT") && !promptActions.ContainsKey("eXIT") && !promptActions.ContainsKey("Quit") && !promptActions.ContainsKey("quit") && !promptActions.ContainsKey("QUIT") && !promptActions.ContainsKey("qUIT") && !promptActions.ContainsKey("Close") && !promptActions.ContainsKey("close") && !promptActions.ContainsKey("CLOSE") && !promptActions.ContainsKey("cLOSE"))
        {
            if (promptActions.Keys.All(x => !x.Contains("Exit", StringComparison.OrdinalIgnoreCase)))
            {
                promptActions.Add($"{promptActions.Count + 1}.\tExit", (s) =>
                {
                    Environment.Exit(0);
                    return Task.CompletedTask;
                });
            }
        }
        
        await PromptUserAsync(promptActions);
    }
    
    private static async Task PromptUserAsync(Dictionary<string, Func<string, Task>> promptActions)
    {
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(promptActions.Keys)
        );
        
        if (promptActions.TryGetValue(option, out var action))
        {
            await action(option);
        }
        
        await PromptUserAsync(promptActions);
    }
    
    public void WriteError(string error) => WriteBox("Error", "Error", error, "Error", Color.Red);

    public async Task<T> WriteProcessingAsync<T>(string userPrompt, Func<UserInput, Task<T>> action) =>
        await AnsiConsole.Status().StartAsync("Processing...", async ctx =>
        {
            ctx.Spinner(Spinner.Known.Star);
            ctx.SpinnerStyle(Style.Parse("green"));
            ctx.Status($"Processing...");
            return await action(new UserInput() {Text = userPrompt});
        });

    public void WriteBox(string banner, string name, string text, string header, Color borderColor)
    {
        AnsiConsole.WriteLine("");
        AnsiConsole.Write(new Rule($"[cyan]{banner}[/]") { Justification = Justify.Center });
        AnsiConsole.WriteLine(name);
        AnsiConsole.Write(new Panel(text)
            .Header(header)
            .Expand()
            .RoundedBorder()
            .BorderColor(borderColor));
        AnsiConsole.WriteLine("");
    }
}