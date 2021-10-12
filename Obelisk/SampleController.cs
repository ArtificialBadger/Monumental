using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monument.Types.Trivial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obelisk
{
    [Route("")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ITrivialSingletonService trivialSingletonService;
        private readonly ITrivialTransientService trivialTransientService;

        public SampleController(ITrivialSingletonService trivialSingletonService, ITrivialTransientService trivialTransientService)
        {
            this.trivialSingletonService = trivialSingletonService;
            this.trivialTransientService = trivialTransientService;
        }

        [HttpGet("")]
        public ActionResult<string> TestEndpoint()
        {
            return Ok($"Singleton: {this.trivialSingletonService.Serve()} Transient: {this.trivialTransientService.Serve()}");
        }
    }
}
