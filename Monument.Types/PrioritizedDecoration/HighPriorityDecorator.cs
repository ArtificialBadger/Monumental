using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.PrioritizedDecoration
{
    [RegistrationPriority(RegistrationPriorityAttribute.HIGH_PRIORITY)]
    public sealed class HighPriorityDecorator : IDecoratableInterface
    {
        public IDecoratableInterface DecoratedInterface { get; }

        public HighPriorityDecorator(IDecoratableInterface decoratedInterface)
        {
            this.DecoratedInterface = decoratedInterface;
        }
    }
}
