using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chinook.Web.Controllers
{
    [Route("~/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        const string secretUser = "dude";
        const string secretPassword = "mysecretpassword";


        [HttpPost]
        public async Task Authenticate([FromForm] string username, [FromForm] string password)
        {
            if (username == secretUser || password == secretPassword)
            {
                //OWASP: Improper Session Handling
                Response.Cookies.Append("IsAuthenticated", "true");

                //Role = admin -> Privilege escalation

                //See other users content.

                //Password forget
            }

            await Task.CompletedTask;
        }
    }
}
