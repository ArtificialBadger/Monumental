using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.PrioritizedDecoration
{
    public sealed class NonPrioritizedDecorator : IDecoratableInterface
    {
        private readonly IDecoratableInterface decoratedInterface;

        public NonPrioritizedDecorator(IDecoratableInterface decoratedInterface)
        {
            this.decoratedInterface = decoratedInterface;
        }
    }
}
