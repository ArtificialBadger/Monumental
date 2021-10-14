using Monument.Types.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.GenericComposite
{
    public class ClosedGenericCompositeDecorator : IGeneric<Animal>
    {
        private readonly IGeneric<Animal> decoratedComponent;

        public ClosedGenericCompositeDecorator(IGeneric<Animal> decoratedComponent)
        {
            this.decoratedComponent = decoratedComponent;
        }
    }
}
