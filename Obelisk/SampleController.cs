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
        private readonly ITrivialScopedService trivialScopedService;
        private readonly ITrivialTransientService trivialTransientService;

        public SampleController(ITrivialSingletonService trivialSingletonService, ITrivialScopedService trivialScopedService, ITrivialTransientService trivialTransientService)
        {
            this.trivialSingletonService = trivialSingletonService;
            this.trivialScopedService = trivialScopedService;
            this.trivialTransientService = trivialTransientService;
        }

        [HttpGet("")]
        public ActionResult<string> TestEndpoint()
        {
            return Ok(new { Singleton = this.trivialSingletonService.Serve(), Scoped = this.trivialScopedService.Serve(), Transient = this.trivialTransientService.Serve() });
        }
    }
}
