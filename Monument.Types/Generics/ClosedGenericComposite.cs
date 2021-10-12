using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types
{
    public class ClosedGenericComposite : IGenericInterface<SimpleImplementation1>
    {
        public IEnumerable<IGenericInterface<SimpleImplementation1>> Inner { get; }
        public ClosedGenericComposite(IEnumerable<IGenericInterface<SimpleImplementation1>> d) { Inner = d; }
    }
}
