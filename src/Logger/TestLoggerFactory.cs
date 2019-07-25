using CrawlerWave.LogTestHelper.Configurations;
using Microsoft.Extensions.Logging;
using System;

namespace CrawlerWave.LogTestHelper.Logger
{
    /// <summary>
    /// A custom LoggerFactory to provides a simple construction of ILogger with ITestSink
    /// </summary>
    public class TestLoggerFactory : ILoggerFactory
    {
        private readonly ITestSink _sink;
        private readonly bool _enabled;

        /// <summary>
        /// Creates a factory and inject the sink and the enable options
        /// </summary>
        /// <param name="sink"></param>
        /// <param name="enabled"></param>
        public TestLoggerFactory(ITestSink sink, bool enabled)
        {
            _sink = sink;
            _enabled = enabled;
        }

        /// <summary>
        /// Creates a new ILoggerd based on category name and injecting the sink and the enable status
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName) => new TestLogger(categoryName, _sink, _enabled);

        /// <summary>
        /// Adds a new provider, but nos supported yet
        /// </summary>
        /// <param name="provider"></param>
        public void AddProvider(ILoggerProvider provider) => throw new NotSupportedException();

        #region IDisposable Support

        private bool disposedValue = false;

        /// <summary>
        /// The default implementation of IDispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// the default implementation of IDispose
        /// </summary>
        /// <param name="disposing"></param>
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