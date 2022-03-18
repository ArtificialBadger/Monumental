using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Monument.Types.AbstractClasses
{
    public abstract class AbstractService
    {
        public abstract Task DoThing();

        public Task DoOtherThing()
        {
            return Task.CompletedTask;
        }

    }
}
