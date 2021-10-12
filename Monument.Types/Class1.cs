using System;
using System.Collections.Generic;

namespace Monument.Types
{
    public class SimpleDecorator : ISimpleInterface
    {
        public ISimpleInterface Inner { get; }
        public SimpleDecorator(ISimpleInterface d) { Inner = d; }
    }
    public class OpenGenericDecorator<T> : IGenericInterface<T>
    {
        public IGenericInterface<T> Inner { get; }

        public OpenGenericDecorator(IGenericInterface<T> d) { Inner = d; }
    }
    public class ClosedGenericDecorator : IGenericInterface<SimpleImplementation1>
    {
        public IGenericInterface<SimpleImplementation1> Inner { get; }

        public ClosedGenericDecorator(IGenericInterface<SimpleImplementation1> d) { this.Inner = d; }
    }
    
    public class ClosedGenericAdapter : IGenericInterface<SimpleImplementation1>
    {
        public ClosedGenericAdapter(IGenericInterface<SimpleImplementation2> inner) { Inner = inner; }

        public IGenericInterface<SimpleImplementation2> Inner { get; }
    }
}
