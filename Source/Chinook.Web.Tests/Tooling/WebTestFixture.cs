using Chinook.Core.DataAccess;
using Chinook.Web.Tests.Tooling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DebuggingAndTesting.Api.Tests.Tooling
{
    public class WebTestFixture
    {
        static readonly SqliteConnection sqliteInMemoryConnection;

        static WebTestFixture()
        {
            //Use package "Microsoft.EntityFrameworkCore.Sqlite" and not the one with .Core suffix.
            sqliteInMemoryConnection = new SqliteConnection("datasource=:memory:");
            //Need to keep the connection open to share it between tests.
            sqliteInMemoryConnection.Open();
        }

        public static TestServer CreateTestServer(string assemblyName, ITestOutputHelper testOutputHelper = null, string databaseName = null, LogLevel logLevel = LogLevel.Debug)
        {
            //We could also use the original startup from the SUT, however there will be no dedicated Startup.cs in .NET 6 anymore.
            var webBuilder = new WebHostBuilder();

            webBuilder.ConfigureServices(services =>
            {
                var assembly = Assembly.Load(assemblyName);
                services.AddControllers().AddApplicationPart(assembly).AddControllersAsServices();

                if (testOutputHelper != null)
                {
                    services.AddSingleton(testOutputHelper);
                    services.AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.AddXUnitLogger();
                    });
                }

                if (databaseName != null)
                {
                    var sqliteConnection = new SqliteConnection($"datasource='Databases/{databaseName}'");
                    services.AddDbContext<ChinookContext>(options =>
                    {
                        options.EnableSensitiveDataLogging();
                        options.UseSqlite(sqliteConnection);
                    });
                }
            });

            webBuilder.ConfigureLogging(logging => logging.SetMinimumLevel(logLevel));

            webBuilder.Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });

            return new TestServer(webBuilder);

            //For advanced scenarios (i.e. JWT authentication) the Alba API testing framework can be helful.
        }

        public static byte[] CreateRandomByteArray(int length)
        {
            var random = new Random();
            var buffer = new byte[length];
            random.NextBytes(buffer);
            return buffer;
        }
    }
}
