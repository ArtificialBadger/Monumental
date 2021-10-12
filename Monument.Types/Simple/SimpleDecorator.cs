using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types
{
    public class SimpleDecorator : ISimpleInterface
    {
        public ISimpleInterface Inner { get; }
        public SimpleDecorator(ISimpleInterface d) { Inner = d; }
    }
}
