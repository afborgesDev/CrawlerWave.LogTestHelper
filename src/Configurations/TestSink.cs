using System.Collections.Concurrent;

namespace CrawlerWave.LogTestHelper.Configurations;

/// <summary>
/// This class provides the methods write and begin scopes for cusotm log
/// </summary>
public class TestSink : ITestSink
{
    private ConcurrentQueue<BeginScopeContext> _scopes;
    private ConcurrentQueue<WriteContext> _writes;

    /// <summary>
    /// Create enabling write / starting a scope
    /// </summary>
    /// <param name="writeEnabled"></param>
    /// <param name="beginEnabled"></param>
    public TestSink(
        Func<WriteContext, bool>? writeEnabled = null,
        Func<BeginScopeContext, bool>? beginEnabled = null)
    {
        WriteEnabled = writeEnabled;
        BeginEnabled = beginEnabled;

        _scopes = new ConcurrentQueue<BeginScopeContext>();
        _writes = new ConcurrentQueue<WriteContext>();
    }

    /// <summary>
    /// event to catch the message that was logged
    /// </summary>
    public event Action<WriteContext>? MessageLogged;

    /// <summary>
    /// event to catch the scope that was started
    /// </summary>
    public event Action<BeginScopeContext>? ScopeStarted;

    /// <summary>
    /// Checks if the writer are enabled
    /// </summary>
    public Func<WriteContext, bool>? WriteEnabled { get; set; }

    /// <summary>
    /// checks if the scoped was started
    /// </summary>
    public Func<BeginScopeContext, bool>? BeginEnabled { get; set; }

    /// <summary>
    /// the list of started scopes
    /// </summary>
    public IProducerConsumerCollection<BeginScopeContext> Scopes { get => _scopes; set => _scopes = new ConcurrentQueue<BeginScopeContext>(value); }

    /// <summary>
    /// the writed items
    /// </summary>
    public IProducerConsumerCollection<WriteContext> Writes { get => _writes; set => _writes = new ConcurrentQueue<WriteContext>(value); }

    /// <summary>
    /// sets the enable for an expecific logger by using the name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool EnableWithTypeName<T>(WriteContext? context)
    {
        if (context is null || string.IsNullOrWhiteSpace(context.LoggerName))
            return false;

        return context.LoggerName.Equals(typeof(T).FullName);
    }

    /// <summary>
    /// sets the enable for an expecific logger by using the type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <returns></returns>
    public static bool EnableWithTypeName<T>(BeginScopeContext context)
    {
        if (context is null || string.IsNullOrWhiteSpace(context.LoggerName))
            return false;

        return context.LoggerName.Equals(typeof(T).FullName);
    }

    /// <inheritdoc/>
    public void Write(WriteContext context)
    {
        if (WriteEnabled == null || WriteEnabled(context))
            _writes.Enqueue(context);
        MessageLogged?.Invoke(context);
    }

    /// <inheritdoc/>
    public void Begin(BeginScopeContext context)
    {
        if (BeginEnabled == null || BeginEnabled(context))
            _scopes.Enqueue(context);
        ScopeStarted?.Invoke(context);
    }
}
