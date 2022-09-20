using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.PrioritizedDecoration
{
    [RegistrationPriority(RegistrationPriorityAttribute.LOW_PRIORITY)]

    public sealed class LowPriorityDecorator : IDecoratableInterface
    {
        private readonly IDecoratableInterface decoratedInterface;

        public LowPriorityDecorator(IDecoratableInterface decoratedInterface)
        {
            this.decoratedInterface = decoratedInterface;
        }
    }
}
