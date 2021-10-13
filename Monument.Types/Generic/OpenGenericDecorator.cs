using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types
{
    public class OpenGenericDecorator<T> : IGenericInterface<T>
    {
        public IGenericInterface<T> Inner { get; }

        public OpenGenericDecorator(IGenericInterface<T> d) { Inner = d; }
    }
}
