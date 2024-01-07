using Frank.SemanticKernel.Examples.Core;

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
        services.AddSingleton<ITextGenerationDemo, TextGenerationDemo>();
    })
    .Build();

var promptService = myHost.Services.GetRequiredService<ITextGenerationDemo>();
await promptService.StartAsync();

Console.WriteLine("Press ENTER key to exit");
Console.ReadLine();