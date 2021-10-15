using Monument.Containers;
using System;
using System.Collections.Generic;

namespace Monument.Conventions
{
    public interface IRegistrationConvention
    {
        IRegisterTimeContainer Register(IEnumerable<Type> types);
    }
}