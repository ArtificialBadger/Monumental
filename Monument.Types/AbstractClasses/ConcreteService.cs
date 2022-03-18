using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Monument.Types.AbstractClasses
{
    public sealed class ConcreteService : AbstractService
    {
        public override Task DoThing()
        {
            throw new NotImplementedException();
        }
    }
}
