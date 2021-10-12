using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types
{
    public class OpenGenericComposite<T> : IGenericInterface<T>
    {
        public IEnumerable<IGenericInterface<T>> Inner { get; }
        public OpenGenericComposite(IEnumerable<IGenericInterface<T>> d) { Inner = d; }
    }
}
