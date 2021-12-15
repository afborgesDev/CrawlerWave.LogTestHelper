using CrawlerWave.LogTestHelper.Configurations;
using CrawlerWave.LogTestHelper.Logger;

namespace CrawlerWave.LogTestHelper;

/// <summary>
/// The initialization Class helpper
/// </summary>
public static class LogTestHelperInitialization
{
    /// <summary>
    /// Creates a customized ITestSink and ILoggerFactory
    /// </summary>
    /// <returns><see cref="ITestSink"/> and <see cref="ILoggerFactory"/></returns>
    public static (ITestSink, ILoggerFactory) Create() => DoCreate();

    /// <summary>
    /// Creates and return a test sink and logger factory implementation for tests
    /// </summary>
    /// <param name="sinkTextWriteContext">
    /// Example: WriteContext context = null; TestSink.MessageLogged += ctx =&gt; context = ctx;
    /// </param>
    /// <returns><see cref="ITestSink"/> and <see cref="ILoggerFactory"/></returns>
    public static (ITestSink, ILoggerFactory) Create(Action<WriteContext> sinkTextWriteContext) => DoCreate(sinkTextWriteContext);

    private static (ITestSink, ILoggerFactory) DoCreate(Action<WriteContext>? sinkTextWriteContext = null)
    {
        var testSink = new TestSink();
        if (sinkTextWriteContext is not null)
            testSink.MessageLogged += sinkTextWriteContext;

        var factory = new TestLoggerFactory(testSink, enabled: true);
        return (testSink, factory);
    }
}
