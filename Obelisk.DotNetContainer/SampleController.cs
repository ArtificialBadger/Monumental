﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monument.Types.Trivial;
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
                new { Singleton = this.trivialSingletonService.Serve(), Scoped = this.trivialScopedService.Serve(), Transient = this.trivialTransientService.Serve() },
                new { Singleton = this.trivialSingletonService.Serve(), Scoped = this.trivialScopedService.Serve(), Transient = this.trivialTransientService.Serve() }
            };
            return Ok(responses);
        }
    }
}
