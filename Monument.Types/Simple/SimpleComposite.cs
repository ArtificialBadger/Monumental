using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Simple
{
    public class SimpleComposite : ISimpleInterface
    {
        public IEnumerable<ISimpleInterface> Inner { get; }
        public SimpleComposite(IEnumerable<ISimpleInterface> simpleImplementations) { Inner = simpleImplementations; }
    }
}
