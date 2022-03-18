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

namespace Obelisk
{
    [Route("Sample")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IConstrainedGeneric<List<Block>, Block> constrainedGeneric;

        private readonly IFactory<ITrivialTransientService> transientServiceFactory;
        private readonly IFactory<IInterface> interfaceFactory;

        private readonly ITrivialSingletonService trivialSingletonService;
        private readonly ITrivialScopedService trivialScopedService;
        private readonly ITrivialTransientService trivialTransientService;

        private readonly IService service;

        public SampleController(IConstrainedGeneric<List<Block>, Block> constrainedGeneric, IFactory<ITrivialTransientService> transientServiceFactory, IFactory<IInterface> interfaceFactory, ITrivialSingletonService trivialSingletonService, ITrivialScopedService trivialScopedService, ITrivialTransientService trivialTransientService, IService service)
        {
            this.constrainedGeneric = constrainedGeneric;
            this.transientServiceFactory = transientServiceFactory;
            this.interfaceFactory = interfaceFactory;
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
                new { Factory = this.transientServiceFactory.Produce().Serve(), Transient = this.trivialTransientService.Serve(), Scoped = this.trivialScopedService.Serve(), Singleton = this.trivialSingletonService.Serve() },
                new { Factory = this.transientServiceFactory.Produce().Serve(), Transient = this.trivialTransientService.Serve(), Scoped = this.trivialScopedService.Serve(), Singleton = this.trivialSingletonService.Serve() },
            };
            return Ok(responses);
        }
    }
}
