using Monument.Attributes;
using Monument.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Obelisk.SimpleInjector.Example
{
    public interface IExampleRepository
    {
        string Resolve();
    }

    public class ExampleRepository : IExampleRepository
    {
        private readonly Guid InstanceId = Guid.NewGuid();

        public string Resolve()
        {
            return $"We got the Data from the Database!!! ID: {InstanceId}";
        }
    }

    public class ExampleRepositoryDecorator : IExampleRepository
    {
        private readonly IExampleRepository exampleRepository;

        public ExampleRepositoryDecorator(IExampleRepository exampleRepository)
        {
            this.exampleRepository = exampleRepository;
        }

        public string Resolve()
        {
            return "Audited: " + this.exampleRepository.Resolve();
        }
    }
}
