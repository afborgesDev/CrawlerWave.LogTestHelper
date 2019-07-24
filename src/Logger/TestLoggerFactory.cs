using CrawlerWave.LogTestHelper.Configurations;
using Microsoft.Extensions.Logging;
using System;

namespace CrawlerWave.LogTestHelper.Logger
{
    public class TestLoggerFactory : ILoggerFactory
    {
        private readonly ITestSink _sink;
        private readonly bool _enabled;

        public TestLoggerFactory(ITestSink sink, bool enabled)
        {
            _sink = sink;
            _enabled = enabled;
        }

        public ILogger CreateLogger(string categoryName) => new TestLogger(categoryName, _sink, _enabled);

        public void AddProvider(ILoggerProvider provider) => throw new NotSupportedException();

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