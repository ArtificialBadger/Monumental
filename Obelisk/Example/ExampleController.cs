using Microsoft.AspNetCore.Mvc;
using Monument.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obelisk.SimpleInjector.Example
{
    [Route("")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IExampleRepository exampleRepository;
        private readonly IFactory<IExampleRepository> factory;

        public ExampleController(IExampleRepository exampleRepository, IFactory<IExampleRepository> factory)
        {
            this.exampleRepository = exampleRepository;
            this.factory = factory;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(this.exampleRepository.Resolve());
        }

        [HttpGet("2")]
        public ActionResult<string> GetMulitple()
        {
            return Ok(this.factory.Produce().Resolve() + " " + this.factory.Produce().Resolve());
        }
    }
}
