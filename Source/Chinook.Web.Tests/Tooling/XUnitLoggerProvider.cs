using Chinook.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Chinook.Web.Tests.Tooling
{
    class XUnitLoggerProvider : ILoggerProvider
    {
        readonly ITestOutputHelper testOutputHelper;

        public XUnitLoggerProvider(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new SimpleOutputLogger(testOutputHelper.WriteLine);
        }

        public void Dispose()
        {
            //Intentionally left blank.
        }
    }

    public static class TestContextLoggerExtensions
    {
        public static ILoggingBuilder AddXUnitLogger(this ILoggingBuilder builder)
        {
            var loggerProvider = ServiceDescriptor.Singleton<ILoggerProvider, XUnitLoggerProvider>();
            builder.Services.Add(loggerProvider);
            return builder;
        }
    }
}
