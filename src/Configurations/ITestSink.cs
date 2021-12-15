using System.Collections.Concurrent;

namespace CrawlerWave.LogTestHelper.Configurations;

/// <summary>
/// This abstraction provides the methods write and begin scopes for cusotm log
/// </summary>

public interface ITestSink
{
    /// <summary>
    /// event to catch the message that was logged
    /// </summary>
    event Action<WriteContext>? MessageLogged;

    /// <summary>
    /// event to catch the scope that was started
    /// </summary>
    event Action<BeginScopeContext>? ScopeStarted;

    /// <summary>
    /// Checks if the writer are enabled
    /// </summary>
    Func<WriteContext, bool>? WriteEnabled { get; set; }

    /// <summary>
    /// checks if the scoped was started
    /// </summary>
    Func<BeginScopeContext, bool>? BeginEnabled { get; set; }

    /// <summary>
    /// the list of started scopes
    /// </summary>
    IProducerConsumerCollection<BeginScopeContext> Scopes { get; set; }

    /// <summary>
    /// the writed items
    /// </summary>
    IProducerConsumerCollection<WriteContext> Writes { get; set; }

    /// <summary>
    /// write a new log
    /// </summary>
    /// <param name="context"></param>
    void Write(WriteContext context);

    /// <summary>
    /// start a new scope
    /// </summary>
    /// <param name="context"></param>
    void Begin(BeginScopeContext context);
}
