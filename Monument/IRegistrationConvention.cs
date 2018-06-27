using System;
using System.Collections.Generic;

namespace Monument
{
    public interface IRegistrationConvention
    {
        IRegisterTimeContainer Register(IEnumerable<Type> types, IRegisterTimeContainer container);
    }
}