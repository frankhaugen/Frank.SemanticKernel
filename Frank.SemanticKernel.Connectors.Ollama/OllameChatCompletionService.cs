using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSharp;
using OllamaSharp.Models;
using OllamaSharp.Streamer;

namespace Frank.SemanticKernel.Connectors.Ollama;

public class OllameChatCompletionService : IChatCompletionService
{
    private readonly OllamaApiClient _client = new OllamaApiClient("http://localhost:11434/api/generate", "llama2-uncensored");
    
    public OllameChatCompletionService()
    {
        Attributes = new Dictionary<string, object?>
        {
            {"model_id", "llama2-uncensored"},
            {"base_url", "http://localhost:11434"},
        };
    }

    public IReadOnlyDictionary<string, object?> Attributes { get; set; }
    
    public async Task<IReadOnlyList<ChatMessageContent>> GetChatMessageContentsAsync(ChatHistory chatHistory, PromptExecutionSettings? executionSettings = null, Kernel? kernel = null, CancellationToken cancellationToken = new CancellationToken())
    {
        var messages = new List<StreamingChatMessageContent>();
        var responseStreamer = new ActionResponseStreamer<ChatResponseStream>(x =>
        {
            messages.Add(new StreamingChatMessageContent(AuthorRole.Assistant, x.Message.Content, modelId: _client.SelectedModel));
        });
        var chat = _client.Chat(responseStreamer);
        
        var responses = await chat.Send(chatHistory.Last(x => x.Role == AuthorRole.User).Content);

        return responses.Select(message => new ChatMessageContent(AuthorRole.Assistant, message.Content, modelId: chat.Model)).ToList().AsReadOnly();
    }

    public async IAsyncEnumerable<StreamingChatMessageContent> GetStreamingChatMessageContentsAsync(ChatHistory chatHistory, PromptExecutionSettings? executionSettings = null, Kernel? kernel = null, CancellationToken cancellationToken = new CancellationToken())
    {
        var messages = new List<StreamingChatMessageContent>();
        var responseStreamer = new ActionResponseStreamer<ChatResponseStream>(x =>
        {
            messages.Add(new StreamingChatMessageContent(AuthorRole.Assistant, x.Message.Content, modelId: _client.SelectedModel));
        });
        var chat = _client.Chat(responseStreamer);
        
        var responses = await chat.Send(chatHistory.Last(x => x.Role == AuthorRole.User).Content);

        foreach (var message in messages)
        {
            yield return new StreamingChatMessageContent(AuthorRole.Assistant, message.Content, modelId: chat.Model);
        }
    }
}