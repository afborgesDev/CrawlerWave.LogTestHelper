﻿using CrawlerWave.LogTestHelper.Configurations;
using CrawlerWave.LogTestHelper.Logger;
using Microsoft.Extensions.Logging;
using System;

namespace CrawlerWave.LogTestHelper
{
    public static class LogTestHelperInitialization
    {
        public static (ITestSink, ILoggerFactory) Create()
        {
            var testSink = new TestSink();
            var factory = new TestLoggerFactory(testSink, enabled: true);
            return (testSink, factory);
        }

        /// <summary>
        /// Creates and return a test sink and logger factory implementation for tests
        /// </summary>
        /// <param name="sinkTextWriteContext">
        /// Example: WriteContext context = null; TestSink.MessageLogged += ctx =&gt; context = ctx;
        /// </param>
        /// <returns></returns>
        public static (ITestSink, ILoggerFactory) Create(Action<WriteContext> sinkTextWriteContext)
        {
            var testSink = new TestSink();
            testSink.MessageLogged += sinkTextWriteContext;
            var factory = new TestLoggerFactory(testSink, enabled: true);
            return (testSink, factory);
        }
    }
}