using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monument.Factories;
using Monument.Types.ConstratinedGeneric;
using Monument.Types.Trivial;
using Monument.Types.Utility;
using Obelisk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obelisk.DotNetContainer
{
    [Route("")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ITrivialSingletonService trivialSingletonService;
        private readonly ITrivialScopedService trivialScopedService;
        private readonly ITrivialTransientService trivialTransientService;
        private readonly IService service;

        public SampleController(ITrivialSingletonService trivialSingletonService, ITrivialScopedService trivialScopedService, ITrivialTransientService trivialTransientService, IService service)
        {
            this.trivialSingletonService = trivialSingletonService;
            this.trivialScopedService = trivialScopedService;
            this.trivialTransientService = trivialTransientService;
            this.service = service;
        }

        [HttpGet("")]
        public ActionResult<string> TestEndpoint()
        {
            var responses = new[]
            {
                new { Transient = this.trivialTransientService.Serve(), Scoped = this.trivialScopedService.Serve(), Singleton = this.trivialSingletonService.Serve() },
                new { Transient = this.trivialTransientService.Serve(), Scoped = this.trivialScopedService.Serve(), Singleton = this.trivialSingletonService.Serve() },
            };
            return Ok(responses);
        }
    }
}
