using CrawlerWave.LogTestHelper.Configurations;
using Microsoft.Extensions.Logging;
using System;

namespace CrawlerWave.LogTestHelper.Logger
{
    /// <summary>
    /// An customized implementation from <see cref="ILoggerProvider"/> ILoggerProvider
    /// to add the ITestSink
    /// </summary>
    public class TestLoggerProvider : ILoggerProvider
    {
        private readonly Func<LogLevel, bool> _filter;

        /// <summary>
        /// Creates the provider and inject the ITestSink
        /// </summary>
        /// <param name="testSink"><see cref="ITestSink"/></param>
        /// <param name="isEnabled">enable or not the logger</param>
        public TestLoggerProvider(ITestSink testSink, bool isEnabled) : this(testSink, _ => isEnabled)
        {
        }

        /// <summary>
        /// Creats the provider and inject the ITestSink but also creates a filter for the registerd logs
        /// </summary>
        /// <param name="testSink"><see cref="ITestSink"/></param>
        /// <param name="filter">the filter for the registered logs on ITestSink</param>
        public TestLoggerProvider(ITestSink testSink, Func<LogLevel, bool> filter)
        {
            Sink = testSink;
            _filter = filter;
        }

        /// <summary>
        /// The sink with all registerd logs
        /// <see cref="ITestSink"/>
        /// </summary>
        public ITestSink Sink { get; }

        /// <summary>
        /// Creates a new ILogger based on Category name and using the sink and the filter 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns><see cref="ILogger"/></returns>
        public ILogger CreateLogger(string categoryName) => new TestLogger(categoryName, Sink, _filter);

        #region IDisposable Support

        private bool disposedValue;

        /// <summary>
        /// the default implementation of IDispose
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