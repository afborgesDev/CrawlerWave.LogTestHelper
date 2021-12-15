namespace CrawlerWave.LogTestHelper.Configurations;

/// <summary>
/// Represents the context for any write method on log
/// </summary>
public class WriteContext
{
    /// <summary>
    /// The level of the writed
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// the event id of the writed
    /// </summary>
    public EventId EventId { get; set; }

    /// <summary>
    /// the state of the writed
    /// </summary>
    public object? State { get; set; }

    /// <summary>
    /// the exception of the writed if exists
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// the method to format all writed
    /// </summary>
    public Func<object, Exception?, string>? Formatter { get; set; }

    /// <summary>
    /// the writed scope
    /// </summary>
    public object? Scope { get; set; }

    /// <summary>
    /// the writed loggername
    /// </summary>
    public string? LoggerName { get; set; }

    /// <summary>
    /// the formated write
    /// </summary>
    public string Message {
        get {
            if (Formatter is not null && this.State is not null)
                return Formatter(this.State, Exception);

            return string.Empty;
        }
    }
}
