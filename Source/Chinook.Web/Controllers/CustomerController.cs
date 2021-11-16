using Chinook.Core.DataAccess;
using Chinook.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ILogger<CustomerController> logger;
        readonly ChinookContext chinookContext;

        public CustomerController(ILogger<CustomerController> logger, ChinookContext chinookContext)
        {
            this.logger = logger;
            this.chinookContext = chinookContext;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            //A tag could also carry the id from the W3C TraceContext.
            return chinookContext.Customers.TagWith("CorrelationId=Id4bc3626f-ae94-4a66-9c4a-9fdc985782a7").Take(4);
        }

        [HttpGet("{lastName}")]
        public IEnumerable<Customer> Get(string lastName)
        {
            return chinookContext.Customers.Where(x => x.LastName == lastName);
        }

        [HttpGet("Delay")]
        public async Task DelayTask(int maxDelay)
        {
            var random = new Random();
            var stopwatch = Stopwatch.StartNew();
            var delay = random.Next(maxDelay);
            await Task.Delay(delay);
            stopwatch.Stop();
            logger.LogInformation("Random task duration: {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
        }
    }
}
