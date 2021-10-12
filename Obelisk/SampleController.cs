using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monument.Types.Trivial;
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
        private readonly ITrivialService trivialService;

        public SampleController(ITrivialService trivialService)
        {
            this.trivialService = trivialService;
        }


        [HttpGet("")]
        public ActionResult<string> TestEndpoint()
        {
            return Ok(this.trivialService.Serve());
        }
    }
}
