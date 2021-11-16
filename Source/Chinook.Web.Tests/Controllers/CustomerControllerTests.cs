using Chinook.Core.Domain;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using DebuggingAndTesting.Api.Tests.Tooling;

namespace Chinook.Web.Controllers.Tests
{
    public sealed class CustomerControllerTests : IDisposable
    {
        readonly TestServer testServer;
        readonly JsonSerializerOptions jsonSerializerOptions;

        public CustomerControllerTests(ITestOutputHelper testOutputHelper)
        {
            testServer = WebTestFixture.CreateTestServer("Chinook.Web", testOutputHelper, "chinook.sqlite");
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [Fact]
        public async Task GetTest()
        {
            var response = await testServer.CreateRequest("api/customer").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var stream = await response.Content.ReadAsStreamAsync();
            var customers = await JsonSerializer.DeserializeAsync<List<Customer>>(stream, jsonSerializerOptions);

            //What is the difference between customers.Count and customers.Count()?
            Assert.True(customers.Count == 4);

            //Consult the test output for potential warnings.
        }

        [InlineData("Köhler")]
        [InlineData("köhler")]
        [InlineData("kohler")]
        [InlineData("silk")]
        [InlineData("Silk")]
        [Theory]
        public async Task GetByLastNameTest(string lastName)
        {
            var response = await testServer.CreateRequest($"api/customer/{lastName}").SendAsync("GET");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var stream = await response.Content.ReadAsStreamAsync();
            var customers = await JsonSerializer.DeserializeAsync<List<Customer>>(stream, jsonSerializerOptions);
            Assert.True(customers.Count == 1);
            var customer = customers.FirstOrDefault();
            Assert.True(customer?.LastName == lastName);
        }
        public void Dispose()
        {
            testServer?.Dispose();
        }
    }
}