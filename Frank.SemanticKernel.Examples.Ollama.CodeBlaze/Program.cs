using Frank.SemanticKernel.Examples.Core;
using Frank.SemanticKernel.Examples.Ollama.CodeBlaze;
using Frank.SemanticKernel.Examples.Ollama.CodeBlaze.Configurations;
using Frank.SemanticKernel.Examples.Ollama.CodeBlaze.Wrappers;

var myHost = new HostBuilder()
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();
        services.Configure<Ollama>(hostContext.Configuration.GetSection(nameof(Ollama)));
        services.AddSingleton<IOllamaClients, OllamaClients>();
        services.AddSingleton<IConsoleHelper, ConsoleHelper>();
        services.AddSingleton <IMenuService, MenuService>();
        services.AddSingleton<ITextGenerationDemo, TextGenerationDemo>();
        services.AddSingleton<IChatDemo, ChatDemo>();
    })
    .Build();

var menuService = myHost.Services.GetRequiredService<IMenuService>();
await menuService.StartAsync();

Console.WriteLine("Press ENTER key to exit");
Console.ReadLine();