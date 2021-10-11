using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obelisk
{
    [Route("[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {

        [HttpGet("")]
        public ActionResult<string> TestEndpoint()
        {
            return Ok("Test Successful");
        }
    }
}
