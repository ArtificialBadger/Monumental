using Monument.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.PrioritizedDecoration
{
    [RegistrationPriority(RegistrationPriorityAttribute.HIGH_PRIORITY)]
    public sealed class HighPriorityDecorator : IDecoratableInterface
    {
        private readonly IDecoratableInterface decoratedInterface;

        public HighPriorityDecorator(IDecoratableInterface decoratedInterface)
        {
            this.decoratedInterface = decoratedInterface;
        }
    }
}
