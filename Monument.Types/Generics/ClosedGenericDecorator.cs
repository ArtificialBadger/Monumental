using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types
{
    public class ClosedGenericDecorator : IGenericInterface<SimpleImplementation1>
    {
        public IGenericInterface<SimpleImplementation1> Inner { get; }

        public ClosedGenericDecorator(IGenericInterface<SimpleImplementation1> d) { this.Inner = d; }
    }
}
