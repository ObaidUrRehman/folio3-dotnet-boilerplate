using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace Folio3.Sbp.UnitTest.Mocks
{
    public class MockLogger<T> : ILogger<T>
    {
        public List<LogItem> Logs { get; } = new List<LogItem>();
        public ITestOutputHelper Output { get; set; }
        public bool ThrowOnError { get; set; }

        /// <summary>
        /// MockLogger Constructor
        /// </summary>
        /// <param name="output">XUnit output sink</param>
        /// <param name="throwOnError">Throw an exception if a Log of level Error or higher comes in</param>
        public MockLogger(ITestOutputHelper output = null, bool throwOnError = false)
        {
            Output = output;
            ThrowOnError = throwOnError;
        }

        public class LogItem
        {
            public DateTime Timestamp { get; set; }
            public LogLevel LogLevel { get; set; }
            public EventId EventId { get; set; }
            public Exception Exception { get; set; }
            public string Message { get; set; }
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public class MockLoggerException : Exception
        {
            public MockLoggerException(string message) : base(message) { }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);

            if (Output != null)
                Output.WriteLine($"MockLogger<{typeof(T).Name}> - [{logLevel}] - [{message}]");

            if (ThrowOnError && logLevel == LogLevel.Error)
                throw new MockLoggerException($"MockLogger recieved error [{message}]");

            Logs.Add(new LogItem
            {
                Timestamp = DateTime.UtcNow,
                LogLevel = logLevel,
                EventId = eventId,
                Exception = exception,
                Message = message,
            });
        }

        public void Clear() => Logs.Clear();
    }
}
