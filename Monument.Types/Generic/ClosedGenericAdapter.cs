using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types
{
    public class ClosedGenericAdapter : IGenericInterface<SimpleImplementation1>
    {
        public ClosedGenericAdapter(IGenericInterface<SimpleImplementation2> inner) { Inner = inner; }

        public IGenericInterface<SimpleImplementation2> Inner { get; }
    }
}
