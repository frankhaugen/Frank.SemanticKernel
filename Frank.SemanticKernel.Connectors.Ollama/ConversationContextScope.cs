namespace Frank.SemanticKernel.Connectors.Ollama;

public sealed class ConversationContextScope : IDisposable
{
    private bool _disposed = false;

    public ConversationContextScope(IEnumerable<long> seed) => Context = new List<long>(seed);
    public ConversationContextScope() => Context = new List<long>();

    public List<long> Context { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed state (managed objects).
                Context.Clear();
            }

            _disposed = true;
        }
    }
}