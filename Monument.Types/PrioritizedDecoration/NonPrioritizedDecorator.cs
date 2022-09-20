using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.PrioritizedDecoration
{
    public sealed class NonPrioritizedDecorator : IDecoratableInterface
    {
        public IDecoratableInterface DecoratedInterface { get; }

        public NonPrioritizedDecorator(IDecoratableInterface decoratedInterface)
        {
            this.DecoratedInterface = decoratedInterface;
        }
    }
}
