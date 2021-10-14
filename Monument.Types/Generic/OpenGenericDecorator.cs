using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Generic
{
    public class OpenGenericDecorator<T> : IGeneric<T>
    {
        public IGeneric<T> DecoratedComponent { get; }

        public OpenGenericDecorator(IGeneric<T> decoratedComponent) { DecoratedComponent = decoratedComponent; }
    }
}
