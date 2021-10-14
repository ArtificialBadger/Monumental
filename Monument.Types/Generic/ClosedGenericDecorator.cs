using Monument.Types.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Generic
{
    public class ClosedGenericDecorator : IGeneric<Animal>
    {
        public IGeneric<Animal> DecoratedComponent { get; }

        public ClosedGenericDecorator(IGeneric<Animal> decoratedComponent)
        {
            this.DecoratedComponent = decoratedComponent;
        }
    }
}
