using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Core.Helpers
{
    public sealed class SimpleOutputLogger : ILogger, IDisposable
    {
        readonly Action<string> outputWriter;

        public SimpleOutputLogger(Action<string> outputWriter)
        {
            this.outputWriter = outputWriter;
        }

        public IDisposable BeginScope<TState>(TState state) => this;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);
            outputWriter.Invoke($"{logLevel}: {message}");
        }

        public void Dispose()
        {
            //Intentionally left blank.
        }
    }
}
