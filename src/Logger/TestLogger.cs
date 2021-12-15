using CrawlerWave.LogTestHelper.Configurations;

namespace CrawlerWave.LogTestHelper.Logger;

/// <summary>
/// The custom implementation of ILogger
/// This implementation intercept each log and register on ITestSink
/// </summary>
public class TestLogger : ILogger
{
    private readonly ITestSink _sink;
    private readonly string? _name;
    private readonly Func<LogLevel, bool> _filter;
    private object? _scope;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="sink"></param>
    /// <param name="enabled"></param>
    public TestLogger(string name, ITestSink sink, bool enabled)
        : this(name, sink, _ => enabled)
    {
    }

    /// <summary>
    /// creates a logger and inject the sink
    /// </summary>
    /// <param name="name"></param>
    /// <param name="sink"></param>
    /// <param name="filter"></param>
    public TestLogger(string name, ITestSink sink, Func<LogLevel, bool> filter)
    {
        _sink = sink;
        _name = name;
        _filter = filter;
    }

    /// <summary>
    /// Name of the Logger
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Starts a scope from ILogger
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="state"></param>
    /// <returns>A TestDisposable for dispose</returns>
    public IDisposable BeginScope<TState>(TState state)
    {
        _scope = state;

        _sink.Begin(new BeginScopeContext() {
            LoggerName = _name,
            Scope = state,
        });

        return TestDisposable.Instance;
    }

    /// <summary>
    /// Register any kind of log and saves on sink
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="logLevel"></param>
    /// <param name="eventId"></param>
    /// <param name="state"></param>
    /// <param name="exception"></param>
    /// <param name="formatter"></param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        _sink.Write(new WriteContext {
            LogLevel = logLevel,
            EventId = eventId,
            State = state,
            Exception = exception,
            Formatter = (s, e) => formatter((TState)s, e),
            LoggerName = _name,
            Scope = _scope
        });
    }

    /// <summary>
    /// Compare if the loglevel are None and differente the filter
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None && _filter(logLevel);

    private class TestDisposable : IDisposable
    {
        public static readonly TestDisposable Instance = new();

        #region IDisposable Support

        private bool disposedValue = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        #endregion IDisposable Support
    }
}
